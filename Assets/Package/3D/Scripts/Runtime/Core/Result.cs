using System;

namespace PhysicsQuery
{
    public readonly struct Result<TElement>
    {
        public static readonly Result<TElement> Empty = new(null, 0);
        public bool IsEmpty => _cache == null || _cache.Length == 0 || _count == 0;
        public int Count => _count;
        public TElement First => Get(0);
        public TElement Last => Get(_count - 1);

        private readonly TElement[] _cache;
        private readonly int _count;

        public Result(TElement[] cache, int count)
        {
            _cache = cache;
            _count = count;
        }

        public readonly TElement Get(int i)
        {
            ValidateIndex(i);
            return _cache[i];
        }
        private void ValidateIndex(int i)
        {
            if (!IsIndexValid(i))
            {
                throw new IndexOutOfRangeException($"Index {i} must be in the range [0, {_count})");
            }
        }
        private readonly bool IsIndexValid(int i)
        {
            return i >= 0 && i < _count;
        }
    }
}