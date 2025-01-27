using System;
using System.Collections;
using System.Collections.Generic;

namespace PhysicsQuery
{
    public readonly struct Result<TElement> : IReadOnlyList<TElement>
    {
        public bool IsEmpty => _cache == null || _cache.Length == 0 || _count == 0;
        public bool IsFull => _cache != null && _cache.Length <= _count;
        public int Count => _count;
        public int Capacity => _cache != null ? _cache.Length : 0;
        public TElement this[int index] => Get(index);
        public TElement First => Get(0);
        public TElement Last => Get(_count - 1);

        private readonly TElement[] _cache;
        private readonly int _count;

        public Result(TElement[] cache, int count)
        {
            _cache = cache;
            _count = count;
        }

        private TElement Get(int i)
        {
            ValidateIndex(i);
            return _cache[i];
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