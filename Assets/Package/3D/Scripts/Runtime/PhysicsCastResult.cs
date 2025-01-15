using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsQuery
{
    public readonly struct PhysicsCastResult
    {
        private struct Comparer : IComparer<RaycastHit>
        {
            public readonly int Compare(RaycastHit x, RaycastHit y)
            {
                return x.distance.CompareTo(y.distance);
            }
        }

        public static PhysicsCastResult Empty => new(null, 0);
        public bool IsEmpty => _hitCache == null || _hitCache.Length == 0 || _count == 0;
        public int Count => _count;
        public RaycastHit ClosestHit => _hitCache[0];
        public RaycastHit FurthestHit => _hitCache[_count - 1];


        private readonly RaycastHit[] _hitCache;
        private readonly int _count;

        public PhysicsCastResult(RaycastHit[] hitCache, int count)
        {
            if (count > 1)
            {
                Array.Sort(hitCache, 0, count, new Comparer());
            }
            _hitCache = hitCache;
            _count = count;
        }

        public readonly RaycastHit Get(int i)
        {
            if (!IsIndexValid(i))
            {
                throw new IndexOutOfRangeException();
            }
            return _hitCache[i];
        }
        private readonly bool IsIndexValid(int i)
        {
            return i >= 0 && i < _count;
        }
    }
}