using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WildHouse
{
    public class MongoCRUD
    {
        private IMongoDatabase db;
        
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public T LoadRecordById<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("id", id);

            return collection.Find(filter).First();
        }

        public T LoadRecordByiName<T>(string table, string name)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("iName", name);

            return collection.Find(filter).First();
        }


        public void UpdateItemQuantity<T>(string table, string id, string quant)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("id", id);
            var update = Builders<T>.Update.Set("quantity", quant);
            collection.UpdateOne(filter, update);
        } 

        public void UpdateGeneral<T>(string table, string id, string toModify, string modified)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("id", id);
            var update = Builders<T>.Update.Set(toModify, modified);
            collection.UpdateOne(filter, update);
        }
        public void DeleteRecord<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("id", id);
            collection.DeleteOne(filter);
        }
    }
}
