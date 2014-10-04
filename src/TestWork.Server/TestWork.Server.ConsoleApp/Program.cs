using System;
using System.IO;
using System.Threading;
using TestWork.Common;
using TestWork.Core.DataInputs;
using TestWork.Core.Storages;
using TestWork.Server.Core;

namespace TestWork.Server.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            
            Console.Write("Запуск сервера...");
            var cmdExecutor = new CommandManager();

            var inFile = new FileStream("test.in", FileMode.Open);
            var input = new StreamReader(inFile);
            IStorage storage = new Storage4();
            var dataInput = new DataInput(input);

            dataInput.SaveDataInfo += storage.Add;
            dataInput.SaveQueryInfo += s => { };

            dataInput.Start();
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(10, 10);

            cmdExecutor.AddCommand("get", new ServerCommandGet(storage, stream => new NetworkDataOutput(stream)));

            var server = new TestWorkServer(new ThreadPoolStrategy(), () => new ServerClient(cmdExecutor));
            server.Start(1117);
            Console.WriteLine(" сервер запущен");

            Console.Write("Для завершения работы нажмите Ввод ");
            Console.ReadLine();
            Console.Write("Завершение работы сервера....");
            server.Stop();
            Console.WriteLine("Работа сервера завершена");
        }
    }
}
