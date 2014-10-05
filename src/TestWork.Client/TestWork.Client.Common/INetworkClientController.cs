using System.Collections.Generic;

namespace TestWork.Client.Common
{

    /// <summary>
    /// Контроллер умеющий общатся с сервером
    /// </summary>
    public interface INetworkClientController
    {

        /// <summary>
        /// Чтение результата обработки команд
        /// </summary>
        /// <returns></returns>
        IList<string> ReadResult();

        /// <summary>
        /// Отправка команды на сервер
        /// </summary>
        /// <param name="name">Команда</param>
        /// <param name="param">Параметры команды</param>
        void WriteCommand(string name, string[] param);
    }
}