using System.Collections.Generic;

namespace TestWork.Common
{
    public interface IMainProcess
    {
        void Process(IDataQueryInput dataInput, IStorage storage, List<string> searchData, IDataOutput dataOutput);
    }
}