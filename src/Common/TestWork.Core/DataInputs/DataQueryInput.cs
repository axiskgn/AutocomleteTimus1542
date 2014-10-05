using System;
using System.IO;
using TestWork.Common;

namespace TestWork.Core.DataInputs
{
    public class DataQueryInput : IDataQueryInput
    {
        private readonly TextReader _textReader;

        public DataQueryInput(TextReader textReader)
        {
            if (textReader == null)
            {
                throw new ArgumentNullException("textReader");
            }
            _textReader = textReader;
        }

        public event Action<string> SaveQueryInfo;

        public void Start()
        {
             var countSearchStr = _textReader.ReadLine();

            if (string.IsNullOrEmpty(countSearchStr))
            {
                throw new FormatException("Ожидались другие данные");
            }

            var countSearchData = int.Parse(countSearchStr);

            for (var i = 0; i < countSearchData; i++)
            {
                SaveQueryInfo(_textReader.ReadLine());
            }
        }
    }
}