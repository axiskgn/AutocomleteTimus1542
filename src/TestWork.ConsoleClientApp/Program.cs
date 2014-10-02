using System.Collections.Generic;
using System.IO;
using TestWork.Core.DataInputs;

namespace TestWork.ConsoleClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var inFile = new FileStream("test.in", FileMode.Open);
            var input = new StreamReader(inFile);
            var dataInput = new DataInput(input);

            var searchData = new List<string>();

            dataInput.SaveDataInfo += (s, i) => { }; 
            dataInput.SaveQueryInfo += searchData.Add;

        }
    }
}
