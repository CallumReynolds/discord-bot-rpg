using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RpgApi.Models
{
    public class User
    {
        // BsonId designates this property as the primary key
        [BsonId]
        // Allows the passing of data types
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;}

        public string Name {get; set;}
    }
}