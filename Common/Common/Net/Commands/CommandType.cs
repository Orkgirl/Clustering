using System;

namespace Common.Net.Commands
{
    [Serializable]
    public enum CommandType
    {
        GetRegion = 1,
        SetRegion = 2,
        GetIndicator = 3,
        SetIndicator = 4,
        GetData = 5,
        SetData = 6
    }
}