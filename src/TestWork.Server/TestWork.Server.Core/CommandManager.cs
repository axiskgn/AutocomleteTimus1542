using System.Collections.Generic;
using System.IO;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class CommandManager : ICommandExecutor
    {
        private readonly Dictionary<string, IServerCommand> _commands = new Dictionary<string, IServerCommand>();

        public void AddCommand(string cmdName, IServerCommand cmd)
        {
            _commands.Add(cmdName, cmd);
        }

        public void ExecCommand(string cmdName, string[] param, INetworkServerController dataOutput)
        {
            var cmd = _commands[cmdName];
            cmd.Exec(param, dataOutput);
        }

    }
}
