using System.Collections.Generic;
using TestWork.Common;

namespace TestWork.Core
{
    public class MainProcess
    {
        /// <summary>
        /// Основная функция
        /// </summary>
        /// <param name="dataQueryInput"></param>
        /// <param name="storage">Хранилище</param>
        /// <param name="searchData">НСИ</param>
        /// <param name="dataOutput">Вывод данных</param>
        /// <param name="dataDictionaryInput"></param>
        public void Process(IDataDictionaryInput dataDictionaryInput, IDataQueryInput dataQueryInput, IStorage storage, List<string> searchData, IDataOutput dataOutput)
        {
            dataDictionaryInput.SaveDataInfo += storage.Add;
            dataQueryInput.SaveQueryInfo += searchData.Add;

            dataDictionaryInput.Start();
            dataQueryInput.Start();

            foreach (var searchStr in searchData)
            {
                var tmpList = storage.Find(searchStr);
                dataOutput.Output(tmpList);
            }
            dataOutput.EndOutput();
        }
    }
}