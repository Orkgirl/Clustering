using System.Collections.Generic;

namespace Server.DataBase.Records
{
    public class DataJSONRecordMap
    {
        public DataJSONRecordMap()
        {
            Items = new List<DataJSONRecord>();
        }

        public List<DataJSONRecord> Items;
    }
}
