using System;

namespace TestWork.Common
{
    public interface IDataDictionaryInput
    {

        /// <summary>
        /// Событие для сохранения НСИ
        /// </summary>
        event Action<string, int> SaveDataInfo;

        /// <summary>
        /// Старт получения данных
        /// </summary>
        void Start(); 
    }
}