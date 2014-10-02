﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using TestWork.Common;
using TestWork.Core;
using TestWork.Core.DataInputs;
using TestWork.Core.DataOutputs;
using TestWork.Core.Storages;

namespace TestWork.ConsoleApp
{
    static class Program
    {
        static void Main()
        {
            var startTime = DateTime.Now;

            #region Точка композиции

            IDataInput dataInput = new DataInput(Console.In);
            IStorage storage = new Storage4();
            IDataOutput dataOutput = new DataOutput(Console.Out);
            var searchData = new List<string>();
            var mainProcess = new MainProcess();

            #endregion

            mainProcess.Process(dataInput, storage, searchData, dataOutput);

            #region Замер производительности

            var endTime = DateTime.Now;
            Debug.WriteLine(endTime - startTime);
            Debug.WriteLine(string.Format("{0},{1}", storage.Count, searchData.Count));

            #endregion
        }


    }
}
