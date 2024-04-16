using DirectBookingOne.Api.DataAccess;
using DirectBookingOne.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectBookingOne.Api.Services
{
    public class AvailabilityService
    {
        private readonly IMongoCollection<Availability> _availabilityCollection;

        public AvailabilityService(IOptions<AvailabilityDataBaseSettings> availabilityDataBaseSettings)
        {
            var _client = new MongoClient(availabilityDataBaseSettings.Value.ConnectionString);
            var _dataBase = _client.GetDatabase(availabilityDataBaseSettings.Value.DatabaseName);
            _availabilityCollection = _dataBase.GetCollection<Availability>(availabilityDataBaseSettings.Value.AvailabilityCollectionName);
        }

        public async Task<bool> CheckAvailability(Availability availability)
        {
            // Check if there's an existing availability for the same product within the same date range
            var filter = Builders<Availability>.Filter.And(
                Builders<Availability>.Filter.Eq(a => a.ProductId, availability.ProductId),
                Builders<Availability>.Filter.Or(
                    Builders<Availability>.Filter.Lte(a => a.StartDate, availability.StartDate) & Builders<Availability>.Filter.Gte(a => a.EndDate, availability.StartDate),
                    Builders<Availability>.Filter.Lte(a => a.StartDate, availability.EndDate) & Builders<Availability>.Filter.Gte(a => a.EndDate, availability.EndDate),
                    Builders<Availability>.Filter.And(
                        Builders<Availability>.Filter.Gte(a => a.StartDate, availability.StartDate),
                        Builders<Availability>.Filter.Lte(a => a.EndDate, availability.EndDate)
                    )
                )
            );

            var existingAvailability = await _availabilityCollection.Find(filter).FirstOrDefaultAsync();

            return existingAvailability == null;
        }

        public async Task<Availability?> CreateAsync(Availability availability)
        {
            if (!await CheckAvailability(availability))
            {
                return null; // Return null if availability overlaps with existing records
            }

            await _availabilityCollection.InsertOneAsync(availability);
            return availability; // Return the created availability for reference
        }

        public async Task<Availability?> GetByIdAsync(string id)
        {
            return await _availabilityCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Availability>> GetByProductIdAsync(string productId)
        {
            return await _availabilityCollection.Find(a => a.ProductId == productId).ToListAsync();
        }

        public async Task UpdateAsync(string id, Availability availability)
        {
            await _availabilityCollection.ReplaceOneAsync(a => a.Id == id, availability);
        }

        public async Task DeleteAsync(string id)
        {
            await _availabilityCollection.DeleteOneAsync(a => a.Id == id);
        }
    }
}
