using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;

namespace PQuery
{
    public abstract class ResultSort<TRaycastHit>
    {
        protected virtual bool WillSort => true;
        private Comparison<TRaycastHit> Comparison => _comparison ??= Compare;

        private static readonly List<TRaycastHit> _sortingCache = new(Settings.DefaultCacheCapacity);
        private static readonly ProfilerMarker _marker = new("Raycast Hit Sort");
        private Comparison<TRaycastHit> _comparison;

        public void Execute(TRaycastHit[] cache, int count)
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
    public class ResultSort_None<TRaycastHit> : ResultSort<TRaycastHit>
    {
        protected override bool WillSort => false;
        protected override int Compare(TRaycastHit a, TRaycastHit b)
        {
            return 0;
        }
    }
    public abstract class ResultSort_Distance<TRaycastHit> : ResultSort<TRaycastHit>
    {
        protected override int Compare(TRaycastHit a, TRaycastHit b)
        {
            return Distance(a).CompareTo(Distance(b));
        }
        protected abstract float Distance(TRaycastHit hit);
    }
}