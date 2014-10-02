using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class TestWorkServer : ITestWorkServer
    {
        private readonly IThreadStrategy _threadStrategy;
        private readonly Func<IServerClient> _serverClientFactory;

        public TestWorkServer(IThreadStrategy threadStrategy, Func<IServerClient> serverClientFactory)
        {
            if (threadStrategy == null) throw new ArgumentNullException("threadStrategy");
            if (serverClientFactory == null) throw new ArgumentNullException("serverClientFactory");
            _threadStrategy = threadStrategy;

            _serverClientFactory = serverClientFactory;
        }

        private readonly AutoResetEvent _endThreadEvent = new AutoResetEvent(false);

        public void Start(int port)
        {
            var lister = new TcpListener(IPAddress.Any, port);

            var thread = new Thread(o =>
            {
                var listerOk = o as TcpListener;
                if (listerOk != null)
                {
                    listerOk.Start();
                    while (true)
                    {
                        if (listerOk.Pending())
                        {
                            _threadStrategy.StartAction(delegate(object obj)
                            {
                                var tcpClient = obj as TcpClient;
                                if (tcpClient != null)
                                {
                                    var client = _serverClientFactory();
                                    client.Start(tcpClient);
                                }

                            }, listerOk.AcceptTcpClient() );
                        }
                        else
                        {
                            if (_endThreadEvent.WaitOne(new TimeSpan(0,0,0,0,100)))
                            {
                                break;
                            }
                        }
                    }
                }

            });
            thread.Start(lister);
        }

        public void Stop()
        {
            _endThreadEvent.Set();
        }

    }
}
