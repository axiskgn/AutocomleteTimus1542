using System.Collections.Generic;

namespace TestWork.Server.Common
{

    /// <summary>
    /// Контроллер умеющий общатся с клиентом
    /// </summary>
    public interface INetworkServerController
    {
        /// <summary>
        /// Читаем команду
        /// </summary>
        /// <param name="cmd">Команда</param>
        /// <param name="param">Параметры</param>
        /// <returns>Успешность чтения</returns>
        bool ReadCommand(out string cmd, out string[] param);

        /// <summary>
        /// Отправка результата
        /// </summary>
        /// <param name="values">Список строк для отправки</param>
        void WriteResultData(IEnumerable<string> values);
    }
}