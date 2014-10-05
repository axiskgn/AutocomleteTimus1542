using System;
using TestWork.Common;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class ServerCommandGet:IServerCommand
    {
        private readonly IStorage _storage;

        public ServerCommandGet(IStorage storage)
        {
            if (storage == null) throw new ArgumentNullException("storage");
            _storage = storage;
        }

        public void Exec(string[] param, INetworkServerController dataOutputStream)
        {
            var values = _storage.Find(param[0]);
            dataOutputStream.WriteResultData(values);

        }
    }
}