using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBCase.Entities
{
    public class ProductImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductImageId { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
