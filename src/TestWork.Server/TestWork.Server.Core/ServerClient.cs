using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
            //var sr = new StreamReader(stream);
            //var cmd = sr.ReadToEnd();

            var request = "";

            var buffer = new byte[1024];

            int count;

            while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
            {

                request += Encoding.ASCII.GetString(buffer, 0, count);

                if (request.Contains(Environment.NewLine) || request.Length > 4096)
                {
                    break;
                }
            }

            request = request.Remove(request.IndexOf(Environment.NewLine, StringComparison.Ordinal));

            if (!string.IsNullOrEmpty(request))
            {
                var param = request.Split(' ');
                _commandExecutor.ExecCommand(param[0], param.Skip(1).ToArray(), stream);
            }
        }
    }
}