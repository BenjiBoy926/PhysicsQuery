using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PQuery
{
    public readonly struct Result<TElement> : IReadOnlyList<TElement>
    {
        public bool IsEmpty => Cache == null || Cache.Length == 0 || _count == 0;
        public bool IsFull => Cache != null && Cache.Length <= _count;
        public int Count => _count;
        public int Capacity => Cache != null ? Cache.Length : 0;
        public TElement this[int index] => Get(index);
        public TElement First => Get(0);
        public TElement Last => Get(_count - 1);

        internal readonly TElement[] Cache;
        private readonly int _count;

        public Result(TElement[] cache, int count)
        {
            Cache = cache;
            _count = count;
        }

        public void Select<TSelected>(TSelected[] destination, Func<TElement, TSelected> selector)
        {
            int count = Mathf.Min(_count, destination.Length);
            for (int i = 0; i < count; i++)
            {
                destination[i] = selector.Invoke(Get(i));
            }
        }
        private TElement Get(int i)
        {
            ValidateIndex(i);
            return Cache[i];
        }
        private void ValidateIndex(int i)
        {
            if (!IsIndexValid(i))
            {
                ThrowIndexOutOfRange(i);
            }
        }
        private void ThrowIndexOutOfRange(int i)
        {
            if (_count > 0)
            {
                throw new IndexOutOfRangeException($"Expected index to be in the range [0, {_count}), but the index is {i}");
            }
            else
            {
                throw new IndexOutOfRangeException($"Cannot access index {i} because the result is empty");
            }
        }
        public bool IsIndexValid(int i)
        {
            return i >= 0 && i < _count;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<TElement> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return Get(i);
            }
        }
    }
}