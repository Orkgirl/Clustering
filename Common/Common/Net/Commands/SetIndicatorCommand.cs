using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Net.Commands
{
    [Serializable]
    public class SetIndicatorCommand : CommandBase
    {
        public Dictionary<int, string> Indicators;
        public SetIndicatorCommand(Dictionary<int, string> indicators) : base(CommandType.SetIndicator)
        {
            Indicators = indicators;
        }
    }
}
