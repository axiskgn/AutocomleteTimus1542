using System;
using System.IO;
using System.Net.Sockets;
using TestWork.Client.Common;

namespace TestWork.Client.Core
{
    public class TestWorkClient : ITestWorkClient
    {
        private readonly string _adress;
        private readonly int _port;
        private readonly Func<Stream, INetworkClientController> _controllerFabric;
        private NetworkStream _stream;
        private TcpClient _tcpClient;

        public TestWorkClient(string adress, int port, Func<Stream, INetworkClientController> controllerFabric )
        {
            if (controllerFabric == null) throw new ArgumentNullException("controllerFabric");
            _adress = adress;
            _port = port;
            _controllerFabric = controllerFabric;
            _stream = null;
        }

        public bool Connect()
        {
            _tcpClient = new TcpClient(_adress, _port);

            _stream = _tcpClient.GetStream();

            return true;
        }

        public bool Connected { get { return _stream != null; } }

        public object ExecCommand(IClientCommand command)
        {
            return command.Exec(_controllerFabric(_stream));
        }

        public void Disconnect()
        {
            
            _stream.Close();
            _stream.Dispose();
            _stream = null;
            _tcpClient.Close();
            }

        public void Dispose()
        {
            if (Connected)
            {
                Disconnect();
            }
        }
    }
}