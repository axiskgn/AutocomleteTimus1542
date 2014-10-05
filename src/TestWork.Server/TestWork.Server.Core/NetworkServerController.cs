using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestWork.Server.Common;

namespace TestWork.Server.Core
{
    public class NetworkServerController : INetworkServerController
    {
        private readonly Stream _stream;

        public NetworkServerController(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            _stream = stream;
        }

        public bool ReadCommand(out string cmd, out string[] param)
        {
            bool result;
            var resultCmd = "";
            var resultParam = default(string[]);

            try
            {
                var request = "";

                var buffer = new byte[1024];

                int count;

                while ((count = _stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    request += Encoding.ASCII.GetString(buffer, 0, count);

                    if (request.Contains(Environment.NewLine) || request.Length > 4096)
                    {
                        break;
                    }
                }

                request = request.Remove(request.IndexOf(Environment.NewLine, StringComparison.Ordinal));

                var paramList = request.Split(' ');
                resultCmd = paramList[0];
                resultParam = paramList.Skip(1).ToArray();
                result = true;
            }
            catch (Exception)
            {
                //Debug.WriteLine(e);
                result = false;
            }

            cmd = resultCmd;
            param = resultParam;
            return result;
        }

        public void WriteResultData(IEnumerable<string> values)
        {
            foreach (var str in values)
            {
                var strForSend = string.Format("{0}{1}", str, Environment.NewLine);
                _stream.Write(Encoding.ASCII.GetBytes(strForSend), 0, strForSend.Length);
            }
            var tmp = Environment.NewLine + Environment.NewLine;
            _stream.Write(Encoding.ASCII.GetBytes(tmp), 0, tmp.Length);

            _stream.Flush();
        }
    }
}