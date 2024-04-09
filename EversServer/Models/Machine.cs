using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EversServer.Models
{
    public class Machine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("category")]
        public string? Category { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("location")]
        public string? Location { get; set; }

        [BsonElement("images")]
        public string[]? Images { get; set; }

        [BsonElement("thumbnail")]
        public string? Thumbnail { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }
    }
}
