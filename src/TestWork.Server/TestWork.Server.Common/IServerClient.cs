using System.Net.Sockets;

namespace TestWork.Server.Common
{

    /// <summary>
    /// ���������� �������� �� ��������� �������
    /// </summary>
    public interface IServerClient
    {
        void Start(TcpClient tcpClient);
    }
}