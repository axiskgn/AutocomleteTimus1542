using System.IO;

namespace TestWork.Server.Common
{
    public interface IServerCommand
    {
        void Exec(string[] patam, Stream dataOutput);
    }
}