using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TestWork.Common;

namespace TestWork.Core.Storages
{

    /// <summary>
    /// Чистый алгоритм генерации бесконечного дерева 
    /// не применим, слишком большие дервья - всё работает медленно
    /// Оставлен с целью построения на его базе других алгоритмов
    /// </summary>
    public class Storage3 :IStorage
    {
        private class Values
        {
            public IList<KeyValuePair<string, int>> List = new List<KeyValuePair<string, int>>();

            public readonly IDictionary<char, Values> Children = new Dictionary<char, Values>();
        }

        private readonly IDictionary<char, Values> _root = new Dictionary<char, Values>();

        public void Add(string text, int cnt)
        {
            AddItem(_root, text, text, cnt);
        }

        private void AddItem(IDictionary<char, Values> root, string wayLeft, string name, int cnt)
        {
            Values value;
            if (!root.ContainsKey(wayLeft[0]))
            {
                value =new Values();
                root.Add(wayLeft[0], value);
            }
            else
            {
                value = root[wayLeft[0]];
            }
            
            value.List.Add(new KeyValuePair<string, int>(name, cnt));
            Count++;
            if (value.List.Count > 10)
            {
                value.List = value.List.OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).ToList();
            }
            if (wayLeft.Length > 1)
            {
                var newWayLeft = wayLeft.Remove(0, 1);
                AddItem(value.Children, newWayLeft, name, cnt);
            }
        }

        public IEnumerable<string> Find(string fnd)
        {
            return FindInItem(_root, fnd);
        }

        private IEnumerable<string> FindInItem(IDictionary<char, Values> root, string wayLeft)
        {
            Values value;
            try
            {
                value = root[wayLeft[0]];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new List<string>();
            }

            if (wayLeft.Length > 1)
            {
                var newWayLeft = wayLeft.Remove(0, 1);
                return FindInItem(value.Children, newWayLeft);
            }
            return value.List.Select(t => t.Key);
        }


        public int Count { get; private set; }
    }
}