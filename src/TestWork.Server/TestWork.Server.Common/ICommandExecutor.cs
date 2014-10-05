namespace TestWork.Server.Common
{

    /// <summary>
    /// Выполнитель команд :)
    /// Хранит и выполняет команды по их назвнию
    /// </summary>
    public interface ICommandExecutor
    {

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="cmdName">Команда</param>
        /// <param name="patam">Параметры команды</param>
        /// <param name="dataOutput">Контроллер для отправки ответа</param>
        void ExecCommand(string cmdName, string[] patam, INetworkServerController dataOutput);
    }
}