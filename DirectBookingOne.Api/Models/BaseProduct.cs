using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DirectBookingOne.Api.Models
{
        public abstract class BaseProduct
        {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        }
    
}
