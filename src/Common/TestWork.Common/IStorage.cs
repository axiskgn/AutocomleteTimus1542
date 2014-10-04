using System.Collections.Generic;

namespace TestWork.Common
{

    /// <summary>
    /// Хранилище НСИ   
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Добавление записи НСИ
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cnt"></param>
        void Add(string text, int cnt);

        /// <summary>
        /// Выборка необходимых данных
        /// </summary>
        /// <param name="fnd"></param>
        /// <returns></returns>
        IEnumerable<string> Find(string fnd);

        /// <summary>
        /// Количество записей в хранилище
        /// </summary>
        int Count { get; }
    }
}