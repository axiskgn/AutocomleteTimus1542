using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TestWork.Client.Core;
using TestWork.Core.DataInputs;
using TestWork.Core.DataOutputs;

namespace TestWork.Client.ConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            int port;
            if (args.Count() < 2 || string.IsNullOrEmpty(args[0]) || !int.TryParse(args[1], out port))
            {
                Console.WriteLine("Формат строки вызова - TestWork.Client.ConsoleApp.exe адресСервера портПодключения");
                return;
            }

            var dataInput = new DataQueryInput(Console.In);
            var startTime = DateTime.Now;
            var searchData = new List<string>();

            dataInput.SaveQueryInfo += searchData.Add;
            dataInput.Start();

            var output = new DataOutput(Console.Out);

            using ( var client = new TestWorkClient(args[0], port,
                    stream => new NetworkClientController(stream)))
            {
                if (client.Connect())
                {
                    foreach (var findValue in searchData)
                    {
                        var command = new CommandGet(findValue);
                        var result = client.ExecCommand(command) as IList<string>;
                        output.Output(result);
                    }
                }
            }
            output.EndOutput();
            var endTime = DateTime.Now;
            Debug.WriteLine("{0}", endTime - startTime);
        }
    }
}
