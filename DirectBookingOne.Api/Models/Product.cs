using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DirectBookingOne.Api.Models
{
    [BsonIgnoreExtraElements]
    public class Product : BaseProduct, IProduct
    {
        [BsonElement("CategoryName")]
        public string CategoryName { get; set; }

        public int Capacity { get; set; }

        public decimal PricePerNight { get; set; }
    }
}
