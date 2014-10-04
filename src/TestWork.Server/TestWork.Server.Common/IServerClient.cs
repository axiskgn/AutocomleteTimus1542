using System.Net.Sockets;

namespace TestWork.Server.Common
{
    public interface IServerClient
    {
        void Start(TcpClient tcpClient);
    }
}