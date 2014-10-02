using System;
using System.Threading;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class ThreadStrategy : IThreadStrategy
    {
        public void StartAction(Action<object> action, object param)
        {
            var thread = new Thread(o => action(o));
            thread.Start(param);;
        }
    }
}