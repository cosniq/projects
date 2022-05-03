using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WildHouse
{
    public class UserModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string quest { get; set; }
        public int balance { get; set; }

        public ContactModel contact { get; set; }

        public AdoptedModel adopted { get; set; }

        public string type { get; set; }
    }
}
