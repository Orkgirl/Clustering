using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Net.Commands
{
    [Serializable]
    public class GetIndicatorCommand : CommandBase
    {
        public GetIndicatorCommand() : base(CommandType.GetIndicator)
        {
        }
    }
}
