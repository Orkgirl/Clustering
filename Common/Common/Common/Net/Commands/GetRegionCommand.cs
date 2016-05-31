using System;
using System.Collections.Generic;

namespace Common.Net.Commands
{
    public enum CommandType
    {
        GetRegion = 1,
        SetRegion = 2,
    }
    [Serializable]
    public abstract class CommandBase
    {
        public CommandType CommandType { private set; get; }

        protected CommandBase(CommandType commandType)
        {
            CommandType = commandType;
        }
    }

    [Serializable]
    public class GetRegionCommand : CommandBase
    {
        public GetRegionCommand() : base(CommandType.GetRegion)
        {

        }
    }

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
