using System;
using System.Collections.Generic;
using System.Linq;
using TestWork.Common;

namespace TestWork.Core.Storages
{

    /// <summary>
    /// Наиболее оптимальный алгоритм
    /// Разновидность построения дерева, ограниченная уровнем ветвления 
    /// (оптимальный уровень выявлен опытным путём = 4
    /// </summary>
    public class Storage4 : IStorage
    {
        /// <summary>
        /// Узел дерева
        /// </summary>
        private class Values
        {
            public IList<KeyValuePair<string, int>> List = new List<KeyValuePair<string, int>>();

            public readonly IDictionary<char, Values> Children = new Dictionary<char, Values>();
        }

        /// <summary>
        /// Уровень вложенности
        /// </summary>
        private const int DepthTree = 4;

        /// <summary>
        /// Корень дерева
        /// </summary>
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
                value = new Values();
                root.Add(wayLeft[0], value);
            }
            else
            {
                value = root[wayLeft[0]];
            }

            if (name.Length - wayLeft.Length <= DepthTree)
            {

                value.List.Add(new KeyValuePair<string, int>(name, cnt));
                if (value.List.Count > 10)
                {
                    value.List =
                        value.List.OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).ToList();
                }
                if (wayLeft.Length > 1)
                {
                    var newWayLeft = wayLeft.Remove(0, 1);
                    AddItem(value.Children, newWayLeft, name, cnt);
                }
                else
                {
                    Count++;
                }
            }
            else
            {
                value.List.Add(new KeyValuePair<string, int>(name, cnt));
                Count++;
            }
        }

        public IEnumerable<string> Find(string fnd)
        {
            return FindInItem(_root, fnd, fnd);
        }

        private IEnumerable<string> FindInItem(IDictionary<char, Values> root, string wayLeft, string name)
        {
            Values value;
            try
            {
                value = root[wayLeft[0]];
            }
            catch (Exception)
            {
                return new List<string>();
            }

            if (name.Length - wayLeft.Length <= DepthTree)
            {
                if (wayLeft.Length > 1)
                {
                    var newWayLeft = wayLeft.Remove(0, 1);
                    return FindInItem(value.Children, newWayLeft, name);
                }
                return value.List.OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).Select(t=>t.Key);
            }
            return value.List.OrderByDescending(t => t.Value).ThenByDescending(k => k.Key).Take(10).Select(t => t.Key);
        }

        public int Count { get; private set; }
    }
}