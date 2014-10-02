using System;
using System.IO;
using TestWork.Common;
using TestWork.Core.DataInputs;
using TestWork.Core.DataOutputs;
using TestWork.Core.Storages;
using TestWork.Server.Core;

namespace TestWork.ConsoleServerApp
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

            cmdExecutor.AddCommand("get", new ServerCommandGet(storage, stream => new DataOutput(new StreamWriter(stream))));

            var server = new TestWorkServer(new ThreadStrategy(), () => new ServerClient(cmdExecutor));
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
