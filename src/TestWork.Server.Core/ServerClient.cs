using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class ServerClient : IServerClient
    {
        private readonly ICommandExecutor _commandExecutor;

        public ServerClient(ICommandExecutor commandExecutor)
        {
            if (commandExecutor == null) throw new ArgumentNullException("commandExecutor");
            _commandExecutor = commandExecutor;
        }

        public void Start(TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();
            var sr = new StreamReader(stream);
            var cmd = sr.ReadLine();
            if (cmd != null)
            {
                var param = cmd.Split(' ');
                _commandExecutor.ExecCommand(param[0], param.Skip(1).ToArray(), stream);
            }
        }
    }
}