using System;

namespace TestWork.Server.Common
{

    /// <summary>
    /// Стратегия управления потоками
    /// </summary>
    public interface IThreadStrategy
    {

        /// <summary>
        /// Запустить ещё один поток
        /// </summary>
        /// <param name="action"></param>
        /// <param name="param"></param>
        void StartAction(Action<object> action, object param);
    }
}