using System;

namespace TestWork.Client.Common
{

    /// <summary>
    /// Сеанс связи с сервером
    /// </summary>
    public interface ITestWorkClient:IDisposable
    {
        /// <summary>
        /// Подключение к серверу
        /// </summary>
        /// <returns></returns>
        bool Connect();

        /// <summary>
        /// Флаг подключения к серверу
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Отправка команды серверу
        /// </summary>
        /// <param name="command">Команда для отправки</param>
        /// <returns>Ответ сервера</returns>
        object ExecCommand(IClientCommand command);

        /// <summary>
        /// Отключение от сервера
        /// </summary>
        void Disconnect();
    }
}