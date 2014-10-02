using System.Collections.Generic;

namespace TestWork.Common
{

    /// <summary>
    /// Вывод данных
    /// </summary>
    public interface IDataOutput
    {
        /// <summary>
        /// Вывод блока данных
        /// </summary>
        /// <param name="data"></param>
        void Output(IEnumerable<string> data);

        /// <summary>
        /// Завершение вывода - для очистки буффера
        /// </summary>
        void EndOutput();
    }
}