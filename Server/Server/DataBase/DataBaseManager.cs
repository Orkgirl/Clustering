using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Server.DataBase.Records;

namespace Server.DataBase
{
    public static class DataBaseManager
    {
        public static RegionJSONRecordMap RegionJsonRecordMap;
        public static IndicatorJSONRecordMap IndicatorJsonRecordMap;
        public static DataJSONRecordMap DataJsonRecordMap;

        public static void Init()
        {
            RegionJsonRecordMap = new RegionJSONRecordMap();
            IndicatorJsonRecordMap = new IndicatorJSONRecordMap();
            DataJsonRecordMap = new DataJSONRecordMap();

            BsonClassMap.RegisterClassMap<RegionJSONRecord>(cm =>
            {
                cm.MapMember(c => c.id_region);
                cm.MapMember(c => c.name_region);
            });

            BsonClassMap.RegisterClassMap<IndicatorJSONRecord>(cm =>
            {
                cm.MapMember(c => c.id_indicator);
                cm.MapMember(c => c.name_indicator);
            });

            BsonClassMap.RegisterClassMap<DataJSONRecord>(cm =>
            {
                cm.MapMember(c => c.id_value);

                cm.MapMember(c => c.id_region);
                cm.MapMember(c => c.id_indicator);
                
                cm.MapMember(c => c.data);
                cm.MapMember(c => c.value);

            });


            var dataBaseAdress = "mongodb://192.168.0.106:27017";
            var dataBaseName = "Clastering";

            Console.WriteLine("Conecting to: " + dataBaseAdress);
            var client = new MongoClient(dataBaseAdress);
            Console.WriteLine("done!");

            Console.WriteLine("Geting db: " + dataBaseName);
            var db = client.GetDatabase(dataBaseName);
            Console.WriteLine("done!");

            //=========================
            //
            //  Regions
            //
            //=========================

            Console.WriteLine("Geting collection: Regions");
            var collectionRegion = db.GetCollection<RegionJSONRecord>("Regions");
            Console.WriteLine("done!");

            Console.WriteLine("Content: ");
            var cursorRegion = collectionRegion.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursorRegion.ToEnumerable())
            {
                RegionJsonRecordMap.Items.Add(document);
                Console.WriteLine(document.id_region + ", " + document.name_region);
            }
            Console.WriteLine("done!");

            //=========================
            //
            //  Indicators
            //
            //=========================

            Console.WriteLine("Geting collection: Indicators");
            var collectionIndicator = db.GetCollection<IndicatorJSONRecord>("Indicators");
            Console.WriteLine("done!");

            Console.WriteLine("Content: ");
            var cursorIndicator = collectionIndicator.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursorIndicator.ToEnumerable())
            {
                IndicatorJsonRecordMap.Items.Add(document);
                Console.WriteLine(document.id_indicator + ", " + document.name_indicator);
            }
            Console.WriteLine("done!");

            //=========================
            //
            //  Data
            //
            //=========================

            Console.WriteLine("Geting collection: Data");
            var collectionData = db.GetCollection<DataJSONRecord>("Data");
            Console.WriteLine("done!");

            Console.WriteLine("Content: ");
            var cursorData = collectionData.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursorData.ToEnumerable())
            {
                DataJsonRecordMap.Items.Add(document);
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", document.id_value, document.id_indicator, document.id_region, document.data, document.value);
            }
            Console.WriteLine("done!");
        }
    }
}
