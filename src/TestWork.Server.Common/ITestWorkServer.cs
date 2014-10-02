namespace TestWork.Server.Common
{
    public interface ITestWorkServer
    {
        void Start(int port);
        void Stop();
    }
}