using System;
using System.Threading;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class ThreadPoolStrategy:IThreadStrategy

    {
        public void StartAction(Action<object> action, object param)
        {
            ThreadPool.QueueUserWorkItem(delegate { action(param); });
        }
    }
}
