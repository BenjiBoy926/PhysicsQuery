using UnityEngine;
using System;

namespace PhysicsQuery
{
    public readonly struct OverlapResult
    {
        public static readonly OverlapResult Empty = new(null, 0);
        public bool IsEmpty => _colliderCache == null || _colliderCache.Length == 0 || _count == 0;

        private readonly Collider[] _colliderCache;
        private readonly int _count;

        public OverlapResult(Collider[] colliderCache, int count)
        {
            _colliderCache = colliderCache;
            _count = count;
        }
        public readonly Collider Get(int index)
        {
            if (!IsIndexValid(index))
            {
                throw new IndexOutOfRangeException();
            }
            return _colliderCache[index];
        }
        private readonly bool IsIndexValid(int index)
        {
            return index >= 0 && index < _count;
        }
    }
}