using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestWork.Common;
using TestWork.Core;
using TestWork.Core.DataInputs;
using TestWork.Core.DataOutputs;
using TestWork.Core.Storages;

namespace TestWork.Test
{
    /// <summary>
    /// Summary description for ComplexTest
    /// </summary>
    [TestClass]
    public class ComplexTest
    {
        /// <summary>
        /// Простой тест на корректность алгоритма
        /// </summary>
        [TestMethod]
        public void CorrectAlgoritmTest()
        {
            #region rawData

            const string rawDataIn = @"5
kare 10
kanojo 20
karetachi 1
korosu 7
sakura 3
3
k
ka
kar
";

            const string rawDataOut = @"kanojo
kare
korosu
karetachi

kanojo
kare
karetachi

kare
karetachi

";

            #endregion

            var input = new StringReader(rawDataIn);

            var sb = new StringBuilder();
            var output = new StringWriter(sb);

            var dataQueryInput = new DataQueryInput(input);
            var dataDictionaryInput = new DataDictionaryInput(input);

            IStorage storage = new Storage4();
            IDataOutput dataOutput = new DataOutput(output);
            var searchData = new List<string>();
            var mainProcess = new MainProcess();

            mainProcess.Process(dataDictionaryInput, dataQueryInput, storage, searchData, dataOutput);

            Assert.AreEqual(rawDataOut, sb.ToString());
        }

        /// <summary>
        /// Тестирование алгоритма на скорость
        /// Тестирование идёт в файл - это намного быстрее чем в консоль (алгоритм должен укладыватся в 4 секунды)
        /// </summary>
        [DeploymentItem("test.in")]
        [TestMethod]
        public void SpeedTest()
        {
            const int maxSec = 5;

            var inFile = new FileStream("test.in", FileMode.Open);
            var outFile = new FileStream("test.out", FileMode.CreateNew);

            var input = new StreamReader(inFile);
            var output = new StreamWriter(outFile);

            var startTime = DateTime.Now;

            var dataQueryInput = new DataQueryInput(input);
            var dataDictionaryInput = new DataDictionaryInput(input);
            IStorage storage = new Storage4();
            IDataOutput dataOutput = new DataOutput(output);
            var searchData = new List<string>();
            var mainProcess = new MainProcess();

            mainProcess.Process(dataDictionaryInput, dataQueryInput, storage, searchData, dataOutput);

            var endTime = DateTime.Now;
            var span = endTime - startTime;
            Debug.WriteLine(span);

            // Вывод в файл намного быстрее чем в консоль
            Assert.IsTrue(span.Seconds < maxSec, string.Format("Слишком долго  - {0}", span));
        }
    }
}
