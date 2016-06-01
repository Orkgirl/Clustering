using System;

namespace Common.Net.Commands
{
    [Serializable]
    public abstract class CommandBase
    {
        public CommandType CommandType { private set; get; }

        protected CommandBase(CommandType commandType)
        {
            CommandType = commandType;
        }
    }
}