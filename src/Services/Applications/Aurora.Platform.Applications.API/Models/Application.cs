using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aurora.Platform.Applications.API.Models
{
    public class Application
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AppId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasCustomConfig { get; set; }
    }
}