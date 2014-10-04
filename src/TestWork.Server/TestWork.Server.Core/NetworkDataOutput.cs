using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestWork.Common;

namespace TestWork.Server.Core
{
    public class NetworkDataOutput:IDataOutput
    {
        private readonly Stream _stream;

        public NetworkDataOutput(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            _stream = stream;
        }

        public void Output(IEnumerable<string> data)
        {
            foreach (var str in data)
            {
                var strForSend = string.Format("{0}{1}", str, Environment.NewLine);
                _stream.Write(Encoding.ASCII.GetBytes(strForSend), 0, strForSend.Length);
            }
            var tmp = Environment.NewLine + Environment.NewLine;
            _stream.Write(Encoding.ASCII.GetBytes(tmp), 0, tmp.Length);
        }

        public void EndOutput()
        {
            _stream.Flush();
        }
    }
}
