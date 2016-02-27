using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Clustering.DataBase
{
    public class DocumentData
    {
        public string Name;
        public Dictionary<string, float> Data;
    }

    public class DataBaseClient : IDataBaseClient
    {
        private string Port = "27017";
        private string IP = "192.168.0.103";
        private string DatabaseName = "Clustering";
        private string CollectionName = "myCollection";

        private MongoClient _client;

        private IMongoDatabase _database;

        private IMongoCollection<BsonDocument> _collection;


        public DataBaseClient()
        {
            _client = new MongoClient("mongodb://" + IP + ":" + Port);

            _database = _client.GetDatabase("Clustering");

            _collection = _database.GetCollection<BsonDocument>("Data");
            
        }

        public void Add(DocumentData data)
        {
            if (data == null)
            {
                throw new Exception("AddToCollection data is null");
                return;
            }
            if (String.IsNullOrEmpty(data.Name))
            {
                throw new Exception("AddToCollection Name is empty");
                return;
            }
            if (data == null || data.Data.Keys.Count == 0)
            {
                throw new Exception("AddToCollection Data is empty");
                return;
            }

            var subDocument = new BsonDocument();

            subDocument.AddRange(data.Data);

            var document = new BsonDocument
            {
                {"name",  data.Name},
                {"data",  subDocument}
            };

            _collection.InsertOne(document);
        }

        public List<DocumentData> GetFromCollection()
        {
            var result = new List<DocumentData>();
            var documents = _collection.Find(new BsonDocument()).ToList();

            foreach (var document in documents)
            {
                var data = new DocumentData();
                data.Name = (string) document["name"];
                data.Data = new Dictionary<string, float>();
                var subDocument = (BsonDocument)document["data"];

                foreach (var keyvalue in subDocument)
                {
                    data.Data.Add(keyvalue.Name, (float)keyvalue.Value);
                }

                result.Add(data);
            }

            return result;

        }
    }
}
