using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Net.Commands
{
    [Serializable]
    public struct SetDataItem
    {
        public int id_value;
        public int id_indicator;
        public int id_region;
        public string data;
        public float value;
    }

    [Serializable]
    public class SetDataCommand : CommandBase
    {
        public List<SetDataItem> Data;
        public SetDataCommand(List<SetDataItem> data) : base(CommandType.SetRegion)
        {
            Data = data;
        }
    }
}