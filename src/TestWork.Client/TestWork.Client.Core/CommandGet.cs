using System;
using TestWork.Client.Common;

namespace TestWork.Client.Core
{

    /// <summary>
    /// Команда поиска
    /// </summary>
    public class CommandGet : IClientCommand
    {
        private readonly string _param;

        public CommandGet(string param)
        {
            if (param == null) throw new ArgumentNullException("param");
            _param = param;
        }

        public object Exec(INetworkClientController controller)
        {
            controller.WriteCommand("get", new[] { _param });
            return controller.ReadResult();            
        }
    }
}