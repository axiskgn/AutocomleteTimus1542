using System.Collections.Generic;

namespace TestWork.Common
{
    public interface IMainProcess
    {
        void Process(IDataInput dataInput, IStorage storage, List<string> searchData, IDataOutput dataOutput);
    }
}