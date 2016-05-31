using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Server
{
    public static class DataBaseManager
    {
        public class RegionJSON
        {

            public Int32 key;
            public String name;
        }

        public static void Init()
        {

            List<RegionJSON> regions = new List<RegionJSON>();

            BsonClassMap.RegisterClassMap<RegionJSON>(cm =>
            {
                cm.MapMember(c => c.key);
                cm.MapMember(c => c.name);
            });


            var client = new MongoClient("mongodb://192.168.0.106:27017");

            var db = client.GetDatabase("Clastering");

            var collection = db.GetCollection<RegionJSON>("Regions");

            var cursor = collection.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                regions.Add(document);
                Console.WriteLine(document.key + ":" + document.name);
            }
        }
    }
}
