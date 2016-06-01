using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Net.Commands
{
    [Serializable]
    public class GetDataCommand : CommandBase
    {
        public string Data;

        public GetDataCommand(string data) : base(CommandType.GetData)
        {
            Data = data;
        }
    }
}

