namespace TestWork.Client.Common
{

    /// <summary>
    /// Команда для отправки на сервер
    /// </summary>
    public interface IClientCommand
    {
        /// <summary>
        /// Отправка команды на сервер
        /// </summary>
        /// <param name="controller">Контроллер умеющий общатся с сервером</param>
        /// <returns>Ответ сервера</returns>
        object Exec(INetworkClientController controller);
    }
}