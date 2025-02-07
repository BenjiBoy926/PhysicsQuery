using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort : IResultSort<RaycastHit>
    {
        protected virtual bool WillSort => true;
        private Comparison<RaycastHit> Comparison => _comparison ??= Compare;

        public static readonly ResultSort None = new ResultSort_None();
        public static readonly ResultSort Distance = new ResultSort_Distance();
        private static readonly List<RaycastHit> _sortingCache = new(Settings.DefaultCacheCapacity);
        private static readonly ProfilerMarker _marker = new("Raycast Hit Sort");
        private Comparison<RaycastHit> _comparison;

        public void Sort(RaycastHit[] cache, int count)
        {
            if (!ShouldSort(cache, count))
            {
                return;
            }
            _marker.Begin();
            List<RaycastHit> list = ReadFromArray(cache, count);
            list.Sort(Comparison);
            WriteToArray(list, cache);
            _marker.End();
        }

        private List<RaycastHit> ReadFromArray(RaycastHit[] cache, int count)
        {
            _sortingCache.Clear();
            for (int i = 0; i < count; i++)
            {
                _sortingCache.Add(cache[i]);
            }
            return _sortingCache;
        }
        private void WriteToArray(List<RaycastHit> list, RaycastHit[] array)
        {
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
        }
        protected bool ShouldSort(RaycastHit[] cache, int count)
        {
            return WillSort && cache != null && cache.Length >= 2 && count >= 2;
        }

        protected abstract int Compare(RaycastHit a, RaycastHit b);
    }
}