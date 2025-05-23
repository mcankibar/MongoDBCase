using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBCase.Entities
{
    public class Product
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryId { get; set; }

        public List<ProductImage> ProductImages { get; set; }

    }
}
