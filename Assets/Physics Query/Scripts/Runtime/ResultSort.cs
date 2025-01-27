using System;
using System.Collections.Generic;
using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort
    {
        public static readonly ResultSort None = new ResultSort_None();
        public static readonly ResultSort Distance = new ResultSort_Distance();

        public abstract void Sort(RaycastHit[] cache, int count);
    }
    internal class ResultSort_None : ResultSort
    {
        public override void Sort(RaycastHit[] cache, int count)
        {
            // Do nothing!
        }
    }
    // NOTE: sorting the array using Array.Sort always allocates memory when invoked. Worse than that,
    // an answer on Stack Overflow suggests that they method is slow irrespective of memory allocation.
    // We probably need to implement our own sorting right here, but which algorithm to use? Apparently,
    // List.Sort varies the algorithm based on the size of the list. Do we want to implement the same thing?
    internal class ResultSort_Distance : ResultSort
    {
        private readonly struct DistanceComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit a, RaycastHit b)
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