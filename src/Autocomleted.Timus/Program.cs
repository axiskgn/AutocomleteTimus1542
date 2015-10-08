using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Autocomleted.Timus
{

    /// <summary>
    /// Решение для отправки на Тимус
    /// </summary>
    class Program
    {

        private class DataValue
        {
            public string Value;
            public int Cnt;
        }

        /// <summary>
        /// Самая простая реализация
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var firstLine = Console.ReadLine();
            var cntInData = int.Parse(firstLine);
            var inList = new List<DataValue>();
            for (var i = 0; i < cntInData; i++)
            {
                var inData = Console.ReadLine().Split(' ');
                inList.Add(new DataValue {Value = inData[0], Cnt = int.Parse(inData[1])});
            }

            var cntOutData = int.Parse(Console.ReadLine());
            for (int i = 0; i < cntOutData; i++)
            {
                var word = Console.ReadLine();
                var find = inList.Where(t => t.Value.StartsWith(word)).OrderByDescending(t => t.Cnt).Take(10);
                foreach (var findItem in find)
                {
                    Console.WriteLine(findItem.Value);
                }
                if (i < cntOutData - 1)
                {
                    Console.WriteLine();
                }
            }

        }
    }
}
