using System;

namespace TestWork.Server.Common
{
    public interface IThreadStrategy
    {
        void StartAction(Action<object> action, object param);
    }
}