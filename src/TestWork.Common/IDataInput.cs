using System;

namespace TestWork.Common
{

    /// <summary>
    /// Получение данных
    /// </summary>
    public interface IDataInput
    {
        /// <summary>
        /// Событие для сохранения НСИ
        /// </summary>
        event Action<string, int> SaveDataInfo;

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