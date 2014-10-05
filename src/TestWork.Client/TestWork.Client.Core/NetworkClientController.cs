using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestWork.Client.Common;

namespace TestWork.Client.Core
{
    public class NetworkClientController : INetworkClientController
    {
        private readonly Stream _stream;

        public NetworkClientController(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            _stream = stream;
        }

        public IList<string> ReadResult()
        {
            var request = "";

            var buffer = new byte[1024];

            int count;

            while ((count = _stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, count);

                if (request.Contains(Environment.NewLine + Environment.NewLine) || request.Length > 4096)
                {
                    break;
                }
            }
            request = request.Remove(request.Count() - 2);
            var result = request.Split(Environment.NewLine.ToCharArray()).Where(t=>!string.IsNullOrEmpty(t)).ToList();

            return result;
        }

        public void WriteCommand(string name, string[] param)
        {
            var cmd = param.Aggregate("", (current, str) => current + string.Format(" {0}", str));

            var strForSend = string.Format("{0}{1}{2}", name, cmd, Environment.NewLine);
            _stream.Write(Encoding.ASCII.GetBytes(strForSend), 0, strForSend.Length);
            _stream.Flush();
        }

    }
}
