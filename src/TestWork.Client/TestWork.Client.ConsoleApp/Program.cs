using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TestWork.Core.DataInputs;

namespace TestWork.Client.ConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            var inFile = new FileStream("test.in", FileMode.Open);
            var input = new StreamReader(inFile);
            var dataInput = new DataInput(input);

            var searchData = new List<string>();

            dataInput.SaveDataInfo += (s, i) => { };
            dataInput.SaveQueryInfo += searchData.Add;

            dataInput.Start();

            //var threadAction = new Action(delegate
            //{
            //    using (var tcpClient = new TcpClient("localhost", 1117))
            //    {
            //        var stream = tcpClient.GetStream();
            //        var sr = new StreamReader(stream);
            //        var sw = new StreamWriter(stream);
            //        var findList = new List<string>(searchData);

            //        foreach (var findValue in findList)
            //        {
            //            sw.WriteLine("get {0}", findValue);
            //            var str = "";
            //            do
            //            {
            //                str = sr.ReadLine();
            //                Console.Write(str);
            //            } while (str!=Environment.NewLine);
            //        }

            //    }
            //});

            //var thread = new Thread(delegate() { threadAction(); });
            //thread.Start();

            //thread.Join();

            var cnt = 0;

            foreach (var findValue in searchData)
            {
                var ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1117);

                var startTime = DateTime.Now;

                var server = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                server.Connect(ipep);

                var stream = new NetworkStream(server);

                var strForSend = string.Format("{0} {1}{2}", "get", findValue, Environment.NewLine);
                stream.Write(Encoding.ASCII.GetBytes(strForSend), 0, strForSend.Length);
                stream.Flush();

                var request = "";

                var buffer = new byte[1024];

                int count;

                while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                {

                    request += Encoding.ASCII.GetString(buffer, 0, count);

                    if (request.Contains(Environment.NewLine + Environment.NewLine) || request.Length > 4096)
                    {
                        break;
                    }
                }
                request = request.Remove(request.Count() - 2);

                var endTime = DateTime.Now;
                Debug.WriteLine(endTime-startTime);
                Console.Write(request);
                cnt++;

                if (cnt > 100)
                {
                    return;
                }

            }

        }
    }
}
