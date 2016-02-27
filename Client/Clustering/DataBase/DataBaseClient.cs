using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Clustering.DB
{
    public static class DataBaseClient
    {
        private static string Port = "27017";
        private static string IP = "192.168.0.103";

        public static void Init()
        {
            var client = new MongoClient("mongodb://" + IP + ":" + Port);

            var database = client.GetDatabase("Clustering");

            var collection = database.GetCollection<BsonDocument>("myCollection");
            
        }
    }
}
