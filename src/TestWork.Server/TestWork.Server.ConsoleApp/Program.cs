using System;
using System.IO;
using System.Linq;
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
            int port;
            if (args.Count() < 2 || string.IsNullOrEmpty(args[0]) || !int.TryParse(args[1], out port))
            {
                Console.WriteLine("Формат строки вызова - TestWork.Server.ConsoleApp.exe файлДанных порт");
                return;
            }

            Console.Write("Запуск сервера...");
            var cmdExecutor = new CommandManager();

            var inFile = new FileStream(args[0], FileMode.Open);
            var input = new StreamReader(inFile);
            IStorage storage = new Storage4();
            var dataInput = new DataDictionaryInput(input);

            dataInput.SaveDataInfo += storage.Add;

            dataInput.Start();

            // тут настраиваем количество доступных потоков
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(100, 100);

            cmdExecutor.AddCommand("get", new ServerCommandGet(storage));

            var server = new TestWorkServer(new ThreadPoolStrategy(), () => new ServerClient(cmdExecutor, stream => new NetworkServerController(stream)));
            server.Start(port);
            Console.WriteLine(" сервер запущен");

            Console.Write("Для завершения работы нажмите Ввод ");
            Console.ReadLine();
            Console.Write("Завершение работы сервера....");
            server.Stop();
            Console.WriteLine("Работа сервера завершена");
        }
    }
}
