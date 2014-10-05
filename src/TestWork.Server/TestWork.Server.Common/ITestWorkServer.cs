namespace TestWork.Server.Common
{

    /// <summary>
    /// Сервер
    /// </summary>
    public interface ITestWorkServer
    {
        /// <summary>
        /// Запустить сервер на указаном порту
        /// </summary>
        /// <param name="port"></param>
        void Start(int port);

        /// <summary>
        /// Остновить сервер
        /// </summary>
        void Stop();
    }
}