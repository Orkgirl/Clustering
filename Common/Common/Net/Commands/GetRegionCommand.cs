using System;

namespace Common.Net.Commands
{
    [Serializable]
    public class GetRegionCommand : CommandBase
    {
        public GetRegionCommand() : base(CommandType.GetRegion)
        {

        }
    }
}
