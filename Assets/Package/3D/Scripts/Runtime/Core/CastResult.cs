using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsQuery
{
    public readonly struct CastResult
    {
        public static readonly CastResult Empty = new(null, 0);
        public bool IsEmpty => _hitCache == null || _hitCache.Length == 0 || _count == 0;
        public int Count => _count;
        public RaycastHit First => Get(0);
        public RaycastHit Last => Get(_count - 1);

        private readonly RaycastHit[] _hitCache;
        private readonly int _count;

        public CastResult(RaycastHit[] hitCache, int count)
        {
            _hitCache = hitCache;
            _count = count;
        }

        public readonly RaycastHit Get(int i)
        {
            ValidateIndex(i);
            return _hitCache[i];
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