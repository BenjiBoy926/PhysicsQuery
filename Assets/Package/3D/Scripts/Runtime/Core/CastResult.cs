using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsQuery
{
    public readonly struct CastResult
    {
        private struct DistanceComparer : IComparer<RaycastHit>
        {
            public readonly int Compare(RaycastHit x, RaycastHit y)
            {
                return x.distance.CompareTo(y.distance);
            }
        }

        private static readonly DistanceComparer Comparer = new();
        public static readonly CastResult Empty = new(null, 0);
        public bool IsEmpty => _hitCache == null || _hitCache.Length == 0 || _count == 0;
        public int Count => _count;
        public RaycastHit ClosestHit => Get(0);
        public RaycastHit FurthestHit => Get(_count - 1);

        private readonly RaycastHit[] _hitCache;
        private readonly int _count;

        public CastResult(RaycastHit[] hitCache, int count)
        {
            if (count > 1)
            {
                Array.Sort(hitCache, 0, count, Comparer);
            }
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