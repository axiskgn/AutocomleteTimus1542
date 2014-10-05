namespace TestWork.Server.Common
{

    /// <summary>
    /// СервернаЯ команда
    /// </summary>
    public interface IServerCommand
    {

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="patam">параметры</param>
        /// <param name="dataOutput">контроллер для ответа</param>
        void Exec(string[] patam, INetworkServerController dataOutput);
    }
}