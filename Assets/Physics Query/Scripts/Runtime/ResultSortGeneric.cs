using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;

namespace PQuery
{
    public abstract class ResultSortGeneric<TRaycastHit>
    {
        protected virtual bool WillSort => true;
        private Comparison<TRaycastHit> Comparison => _comparison ??= Compare;

        private static readonly List<TRaycastHit> _sortingCache = new(Settings.DefaultCacheCapacity);
        private static readonly ProfilerMarker _marker = new("Raycast Hit Sort");
        private Comparison<TRaycastHit> _comparison;

        public void Sort(TRaycastHit[] cache, int count)
        {
            if (!ShouldSort(cache, count))
            {
                return;
            }
            _marker.Begin();
            List<TRaycastHit> list = ReadFromArray(cache, count);
            list.Sort(Comparison);
            WriteToArray(list, cache);
            _marker.End();
        }

        private List<TRaycastHit> ReadFromArray(TRaycastHit[] cache, int count)
        {
            _sortingCache.Clear();
            for (int i = 0; i < count; i++)
            {
                _sortingCache.Add(cache[i]);
            }
            return _sortingCache;
        }
        private void WriteToArray(List<TRaycastHit> list, TRaycastHit[] array)
        {
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
        }
        protected bool ShouldSort(TRaycastHit[] cache, int count)
        {
            return WillSort && cache != null && cache.Length >= 2 && count >= 2;
        }

        protected abstract int Compare(TRaycastHit a, TRaycastHit b);
    }
}