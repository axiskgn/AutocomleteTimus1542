using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestWork.Client.Core;
using TestWork.Server.Core;

namespace TestWork.Test
{

    /// <summary>
    /// Тестирование алгоритмов сетевого обмена
    /// </summary>
    [TestClass]
    public class NetworkUnitTest
    {
        [TestMethod]
        public void CommandReadWriteTest()
        {

            var stream = new MemoryStream();

            var clientController = new NetworkClientController(stream);
            clientController.WriteCommand("get", new []{"1","2"});

            stream.Position = 0;

            var serverController = new NetworkServerController(stream);
            string cmd;
            string[] param;
            Assert.IsTrue(serverController.ReadCommand(out cmd, out param));

            Assert.AreEqual("get", cmd);
            Assert.AreEqual("1", param[0]);
            Assert.AreEqual("2", param[1]);
        }

        [TestMethod]
        public void ResultReadWriteTest()
        {

            var stream = new MemoryStream();

            var serverController = new NetworkServerController(stream);
            serverController.WriteResultData(new List<string>{"123","456","789"});

            stream.Position = 0;

            var clientController = new NetworkClientController(stream);
            var result = clientController.ReadResult();

            Assert.AreEqual("123", result[0]);
            Assert.AreEqual("456", result[1]);
            Assert.AreEqual("789", result[2]);
        }
    }
}
