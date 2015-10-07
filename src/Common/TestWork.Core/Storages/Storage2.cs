using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TestWork.Common;

namespace TestWork.Core.Storages
{

    /// <summary>
    /// Наиболее быстрый алгоритм 
    /// Не пригодный т.к. - сложно поддерживать
    /// может применятся если ну очень надо ))
    /// </summary>
    public class Storage2 :IStorage
    {
        private readonly Dictionary<char, Dictionary<char, Dictionary<char, Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>>>> _storage = 
            new Dictionary<char, Dictionary<char, Dictionary<char, Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>>>>();

        public void Add(string text, int cnt)
        {
            var char0 = text[0];
            var char1 = text.Length < 2 ? ' ' : text[1];
            var char2 = text.Length < 3 ? ' ' : text[2];
            var char3 = text.Length < 4 ? ' ' : text[3];
            var char4 = text.Length < 5 ? ' ' : text[4];

            Dictionary<char,Dictionary<char,Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>>> storage0;
            if (_storage.ContainsKey(char0))
            {
                storage0 = _storage[char0];
            }
            else
            {
                storage0 = new Dictionary<char,Dictionary<char,Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>>>();
                _storage.Add(char0, storage0);
            }

            Dictionary<char,Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>> storage1;
            if (storage0.ContainsKey(char1))
            {
                storage1 = storage0[char1];
            }
            else
            {
                storage1 = new Dictionary<char,Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>>();
                storage0.Add(char1, storage1);
                if (char1 != ' ' && !storage0.ContainsKey(' '))
                {
                    var nullArray1 =
                        new Dictionary<char, Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>>();
                    storage0.Add(' ',nullArray1);
                    var nullArray2 =new Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>();
                    nullArray1.Add(' ',nullArray2);
                    var nullArray3 = new Dictionary<char, List<KeyValuePair<string, int>>>();
                    nullArray2.Add(' ', nullArray3);
                    var nullArray4 = new List<KeyValuePair<string, int>>();
                    nullArray3.Add(' ', nullArray4);

                }
            }

            Dictionary<char,Dictionary<char, List<KeyValuePair<string, int>>>> storage2;
            if (storage1.ContainsKey(char2))
            {
                storage2 = storage1[char2];
            }
            else
            {
                storage2 = new Dictionary<char,Dictionary<char, List<KeyValuePair<string, int>>>>();
                storage1.Add(char2, storage2);
                if (char2 != ' ' && !storage1.ContainsKey(' '))
                {
                    var nullArray2 = new Dictionary<char, Dictionary<char, List<KeyValuePair<string, int>>>>();
                    storage1.Add(' ', nullArray2);
                    var nullArray3 = new Dictionary<char, List<KeyValuePair<string, int>>>();
                    nullArray2.Add(' ', nullArray3);
                    var nullArray4 = new List<KeyValuePair<string, int>>();
                    nullArray3.Add(' ', nullArray4);
                }
            }

            Dictionary<char,List<KeyValuePair<string, int>>> storage3;
            if (storage2.ContainsKey(char3))
            {
                storage3 = storage2[char3];
            }
            else
            {
                storage3 = new Dictionary<char,List<KeyValuePair<string, int>>>();
                storage2.Add(char3, storage3);
                if (char3 != ' ' && !storage2.ContainsKey(' '))
                {
                    var nullArray3 = new Dictionary<char, List<KeyValuePair<string, int>>>();
                    storage2.Add(' ', nullArray3);
                    var nullArray4 = new List<KeyValuePair<string, int>>();
                    nullArray3.Add(' ', nullArray4);
                }
            }

            List<KeyValuePair<string, int>> storage4;
            if (storage3.ContainsKey(char4))
            {
                storage4 = storage3[char4];
            }
            else
            {
                storage4 = new List<KeyValuePair<string, int>>();
                storage3.Add(char4, storage4);
                if (char4 != ' ' && !storage3.ContainsKey(' '))
                {
                    storage3.Add(' ', new List<KeyValuePair<string, int>>());
                }
            }

            var val = new KeyValuePair<string, int>(text, cnt);
            storage4.Add(val);

            if (char4 != ' ')
            {
                var tmp = storage3[' '];
                tmp.Add(val);
                if (tmp.Count > 10)
                {
                    storage3[' '] = tmp.OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).ToList();
                }
            }

            if (char3 != ' ')
            {
                var tmp = storage2[' '][' '];
                tmp.Add(val);
                if (tmp.Count > 10)
                {
                    storage2[' '][' '] = tmp.OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).ToList();
                }
            }

            if (char2 != ' ')
            {
                var tmp = storage1[' '][' '][' '];
                tmp.Add(val);
                if (tmp.Count > 10)
                {
                    storage1[' '][' '][' '] = tmp.OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).ToList();
                }
            }

            if (char1 != ' ')
            {
                var tmp = storage0[' '][' '][' '][' '];
                tmp.Add(val);
                if (tmp.Count > 10)
                {
                    storage0[' '][' '][' '][' '] = tmp.OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).ToList();
                }
            }

            Count++;
        }

        public IEnumerable<string> Find(string text)
        {
            var char0 = text[0];
            var char1 = text.Length < 2 ? ' ' : text[1];
            var char2 = text.Length < 3 ? ' ' : text[2];
            var char3 = text.Length < 4 ? ' ' : text[3];
            var char4 = text.Length < 5 ? ' ' : text[4];

            List<KeyValuePair<string, int>> storage;

            try
            {
                storage = _storage[char0][char1][char2][char3][char4];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return new List<string>();
            }

            return storage.Where(t => t.Key.StartsWith(text)).OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).Select(t => t.Key);
        }

        public int Count { get; private set; }
    }
}