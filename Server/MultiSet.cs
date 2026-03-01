using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// 중복집합 구현체
    /// </summary>
    public sealed class MultiSet<T> : IEnumerable<T> where T : notnull
    {
        private readonly Dictionary<T, int> _dictionary = new();
        public int TotalCount { get; private set; }


        public MultiSet() { }

        public MultiSet(IEnumerable<T> origin)
        {
            foreach(var item in origin)
            {
                Add(item);
            }
        }


        public void Add(T item, int amount = 1)
        {
            if (amount < 1) return;

            _dictionary.TryGetValue(item, out int currentCount);
            _dictionary[item] = currentCount + amount;
            TotalCount += amount;
        }

        public bool Remove(T item, int amount = 1)
        {
            if (amount <= 0 || !_dictionary.TryGetValue(item, out int currentCount) || currentCount < amount)
                return false;

            if (currentCount == amount)
                _dictionary.Remove(item);
            else
                _dictionary[item] = currentCount - amount;

            TotalCount -= amount;
            return true;
        }

        public void Clear()
        {
            _dictionary.Clear();
            TotalCount = 0;
        }

        /// <summary>
        /// 특정 원소의 개수 가져오기
        /// </summary>
        public int CountOf(T item)
        {
            return _dictionary.GetValueOrDefault(item, defaultValue: 0);
        }

        public bool Contains(T item) => _dictionary.ContainsKey(item);

        /// <summary>
        /// 각 원소의 개수들을 순회하며 가져오기
        /// </summary>
        public IEnumerable<(T value, int count)> GetItemCounts()
        {
            foreach (var pair in _dictionary)
            {
                yield return (pair.Key, pair.Value);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var pair in _dictionary)
            {
                for (int i = 0; i < pair.Value; i++)
                    yield return pair.Key;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public T[] ToArray()
        {
            T[] array = new T[TotalCount];

            int c = 0;
            foreach(var item in this)
            {
                array[c++] = item;
            }

            return array;
        }
    }
}
