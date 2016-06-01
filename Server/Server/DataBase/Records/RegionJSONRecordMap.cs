using System.Collections.Generic;

namespace Server.DataBase.Records
{
    public class RegionJSONRecordMap
    {
        public RegionJSONRecordMap()
        {
            Items = new List<RegionJSONRecord>();
        }

        public List<RegionJSONRecord> Items;
    }
}
