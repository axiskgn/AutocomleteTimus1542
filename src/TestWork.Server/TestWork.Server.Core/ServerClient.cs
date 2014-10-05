using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class ServerClient : IServerClient
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly Func<Stream, INetworkServerController> _networkControllerFabric;

        public ServerClient(ICommandExecutor commandExecutor, Func<Stream,INetworkServerController> networkControllerFabric)
        {
            if (commandExecutor == null) throw new ArgumentNullException("commandExecutor");
            if (networkControllerFabric == null) throw new ArgumentNullException("networkControllerFabric");
            _commandExecutor = commandExecutor;
            _networkControllerFabric = networkControllerFabric;
        }

        public void Start(TcpClient tcpClient)
        {

            var startTime = DateTime.Now;

            var stream = tcpClient.GetStream();

            var networkController = _networkControllerFabric(stream);

            while (true)
            {
                string cmd;
                string[] param;
                if (networkController.ReadCommand(out cmd, out param))
                {
                    _commandExecutor.ExecCommand(cmd, param, networkController);
                }
                else
                {
                    var endTime = DateTime.Now;
                    Debug.WriteLine(endTime-startTime);
                    break;
                }
            }
        }

    }
}