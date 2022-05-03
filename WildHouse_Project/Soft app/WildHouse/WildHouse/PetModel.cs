using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WildHouse
{
    public class PetModel
    {
        [BsonRepresentation(BsonType.ObjectId)]

        public string id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string category { get; set; }
        public string species { get; set; }
        public string breed { get; set; }
        public string age { get; set; }
        public string adoption { get; set; }
        public string health { get; set; }
        public string questid { get; set; }

        public string description { get; set; }
    }
}
