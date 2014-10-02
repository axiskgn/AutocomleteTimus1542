using System;
using System.IO;
using TestWork.Common;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class ServerCommandGet:IServerCommand
    {
        private readonly IStorage _storage;
        private readonly Func<Stream, IDataOutput> _dataOutputFabric;

        public ServerCommandGet(IStorage storage, Func<Stream,IDataOutput>  dataOutputFabric)
        {
            if (storage == null) throw new ArgumentNullException("storage");
            if (dataOutputFabric == null) throw new ArgumentNullException("dataOutputFabric");
            _storage = storage;
            _dataOutputFabric = dataOutputFabric;
        }

        public void Exec(string[] param, Stream dataOutputStream)
        {
            var values = _storage.Find(param[0]);
            var dataOutput = _dataOutputFabric(dataOutputStream);
            dataOutput.Output(values);
            dataOutput.EndOutput();
        }
    }
}