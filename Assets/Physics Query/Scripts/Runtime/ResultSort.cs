using System;
using System.Collections.Generic;
using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort
    {
        public abstract void Sort(RaycastHit[] cache, int count);

        public static readonly ResultSort None = new ResultSort_None();
        public static readonly ResultSort Distance = new ResultSort_Distance();
    }
    internal class ResultSort_None : ResultSort
    {
        public override void Sort(RaycastHit[] cache, int count)
        {
            // Do nothing!
        }
    }
    internal class ResultSort_Distance : ResultSort
    {
        private readonly struct DistanceComparer : IComparer<RaycastHit>
        {
            public readonly int Compare(RaycastHit a, RaycastHit b)
            {
                return a.distance.CompareTo(b.distance);
            }
        }
        private static readonly DistanceComparer Comparer = new();

        public override void Sort(RaycastHit[] cache, int count)
        {
            if (count >= 2)
            {
                Array.Sort(cache, 0, count, Comparer);
            }
        }
    }
}