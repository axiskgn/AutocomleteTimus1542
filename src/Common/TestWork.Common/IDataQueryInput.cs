using System;

namespace TestWork.Common
{

    /// <summary>
    /// Получение данных
    /// </summary>
    public interface IDataQueryInput
    {


        /// <summary>
        /// Событие для сохранения данных запроса
        /// </summary>
        event Action<string> SaveQueryInfo;

        /// <summary>
        /// Старт получения данных
        /// </summary>
        void Start();
    }
}