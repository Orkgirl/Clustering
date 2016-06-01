using System.Collections.Generic;

namespace Server.DataBase.Records
{
    public class IndicatorJSONRecordMap
    {
        public IndicatorJSONRecordMap()
        {
            Items = new List<IndicatorJSONRecord>();
        }

        public List<IndicatorJSONRecord> Items;
    }
}
