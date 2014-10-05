using System.Net.Sockets;

namespace TestWork.Server.Common
{

    /// <summary>
    /// Обработчик клиентов на серверной стороне
    /// </summary>
    public interface IServerClient
    {
        void Start(TcpClient tcpClient);
    }
}