using System.Collections.Generic;
using TestWork.Common;

namespace TestWork.Core
{
    public class MainProcess
    {
        /// <summary>
        /// Основная функция
        /// </summary>
        /// <param name="dataInput">Источник данных</param>
        /// <param name="storage">Хранилище</param>
        /// <param name="searchData">НСИ</param>
        /// <param name="dataOutput">Вывод данных</param>
        public void Process(IDataInput dataInput, IStorage storage, List<string> searchData, IDataOutput dataOutput)
        {
            dataInput.SaveDataInfo += storage.Add;
            dataInput.SaveQueryInfo += searchData.Add;

            dataInput.Start();

            foreach (var searchStr in searchData)
            {
                var tmpList = storage.Find(searchStr);
                dataOutput.Output(tmpList);
            }
            dataOutput.EndOutput();
        }
    }
}