using System;
using System.Collections.Generic;
using System.IO;
using TestWork.Common;

namespace TestWork.Core.DataOutputs
{
    public class DataOutput:IDataOutput
    {
        private readonly TextWriter _writter;

        private string _buff = "";

        public DataOutput(TextWriter writter)
        {
            if (writter == null)
            {
                throw new ArgumentNullException("writter");
            }
            _writter = writter;
        }

        public void Output(IEnumerable<string> data)
        {
            foreach (var str in data)
            {
                _buff += str + Environment.NewLine;
            }
            _buff += Environment.NewLine;
            if (_buff.Length > 300)
            {
                _writter.Write(_buff);
                _buff = "";
            }

        }

        public void EndOutput()
        {
            if (!string.IsNullOrEmpty(_buff))
            {
                _writter.Write(_buff);
                _buff = "";
            }
        }
    }
}