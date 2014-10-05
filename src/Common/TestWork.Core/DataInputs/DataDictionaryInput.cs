using System;
using System.IO;
using TestWork.Common;

namespace TestWork.Core.DataInputs
{
    public class DataDictionaryInput : IDataDictionaryInput
    {

        private readonly TextReader _textReader;

        public DataDictionaryInput(TextReader textReader)
        {
            if (textReader == null)
            {
                throw new ArgumentNullException("textReader");
            }
            _textReader = textReader;
        }


        public event Action<string, int> SaveDataInfo;

        public void Start()
        {
            var countDataStr = _textReader.ReadLine();
            if (string.IsNullOrEmpty(countDataStr))
            {
                throw new FormatException("Ожидались другие данные");
            }

            var countInData = int.Parse(countDataStr);

            for (var i = 0; i < countInData; i++)
            {
                var rawData = _textReader.ReadLine();
                if (string.IsNullOrEmpty(rawData))
                {
                    throw new FormatException("Ожидались другие данные");
                }
                var arr = rawData.Split(' ');
                var cnt = int.Parse(arr[1]);
                SaveDataInfo(arr[0], cnt);
            }

        }
    }
}

