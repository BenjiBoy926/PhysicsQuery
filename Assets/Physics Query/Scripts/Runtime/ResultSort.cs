using System;
using System.Collections.Generic;
using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort : IComparer<RaycastHit>
    {
        protected virtual bool WillSort => true;

        public static readonly ResultSort None = new ResultSort_None();
        public static readonly ResultSort Distance = new ResultSort_Distance();
        private static readonly List<RaycastHit> _sortingCache = new(Settings.DefaultCacheCapacity);

        public void Sort(RaycastHit[] cache, int count)
        {
            if (!ShouldSort(cache, count))
            {
                return;
            }
            List<RaycastHit> list = ReadFromArray(cache, count);
            list.Sort(this);
            WriteToArray(list, cache);
        }
        private static List<RaycastHit> ReadFromArray(RaycastHit[] cache, int count)
        {
            _sortingCache.Clear();
            for (int i = 0; i < count; i++)
            {
                _sortingCache.Add(cache[i]);
            }
            return _sortingCache;
        }
        private static void WriteToArray(List<RaycastHit> list, RaycastHit[] array)
        {
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
        }
        private bool ShouldSort(RaycastHit[] cache, int count)
        {
            return WillSort && cache != null && cache.Length >= 2 && count >= 2;
        }

        public abstract int Compare(RaycastHit a, RaycastHit b);
    }
    internal class ResultSort_None : ResultSort
    {
        protected override bool WillSort => false;
        public override int Compare(RaycastHit a, RaycastHit b)
        {
            return 0;
        }
    }
    internal class ResultSort_Distance : ResultSort
    {
        public override int Compare(RaycastHit a, RaycastHit b)
        {
            return a.distance.CompareTo(b.distance);
        }
    }
}