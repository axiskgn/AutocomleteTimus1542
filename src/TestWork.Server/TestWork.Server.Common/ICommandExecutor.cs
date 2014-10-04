using System.IO;

namespace TestWork.Server.Common
{
    public interface ICommandExecutor
    {
        void ExecCommand(string cmdName, string[] patam, Stream dataOutput);
    }
}