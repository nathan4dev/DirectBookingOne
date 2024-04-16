using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DirectBookingOne.Api.Models
{
    [BsonIgnoreExtraElements]
    public class Availability
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // Make it nullable

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }  // Reference to the Product

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime StartDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime EndDate { get; set; }
    }
}
