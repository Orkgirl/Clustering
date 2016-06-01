using System;
using System.Collections.Generic;

namespace Common.Net.Commands
{
    [Serializable]
    public class SetRegionCommand : CommandBase
    {
        public Dictionary<int, string> Regions; 
        public SetRegionCommand(Dictionary<int, string> regions) : base(CommandType.SetRegion)
        {
            Regions = regions;
        }
    }
}