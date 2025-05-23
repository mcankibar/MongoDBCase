using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBCase.Entities
{
    public class Slider
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string SliderId { get; set; }
        public string SliderTitle { get; set; }
        public string SliderSubTitle { get; set; }
        public string SliderImageUrl { get; set; }

        
    }
}
