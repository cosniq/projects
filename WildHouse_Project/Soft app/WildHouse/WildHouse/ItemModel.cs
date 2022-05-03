using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WildHouse
{
    public class ItemModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string iName { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string description { get; set; }
    }
}
