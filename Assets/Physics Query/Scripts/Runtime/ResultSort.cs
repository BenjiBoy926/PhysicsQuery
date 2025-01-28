using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort
    {
        protected virtual bool WillSort => true;

        public static readonly ResultSort None = new ResultSort_None();
        public static readonly ResultSort Distance = new ResultSort_Distance();

        public abstract void Sort(RaycastHit[] cache, int count);
        protected bool ShouldSort(RaycastHit[] cache, int count)
        {
            return WillSort && cache != null && cache.Length >= 2 && count >= 2;
        }
    }
    public abstract class ResultSort<TWrapper> : ResultSort where TWrapper : IComparable<TWrapper>
    {
        private static readonly List<TWrapper> _sortingCache = new(Settings.DefaultCacheCapacity);
        private static readonly ProfilerMarker _marker = new("Raycast Hit Sort");

        public override void Sort(RaycastHit[] cache, int count)
        {
            if (!ShouldSort(cache, count))
            {
                return;
            }
            _marker.Begin();
            List<TWrapper> list = ReadFromArray(cache, count);
            list.Sort();
            WriteToArray(list, cache);
            _marker.End();
        }

        private List<TWrapper> ReadFromArray(RaycastHit[] cache, int count)
        {
            _sortingCache.Clear();
            for (int i = 0; i < count; i++)
            {
                TWrapper item = Wrap(cache[i]);
                _sortingCache.Add(item);
            }
            return _sortingCache;
        }
        private void WriteToArray(List<TWrapper> list, RaycastHit[] array)
        {
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = Unwrap(list[i]);
            }
        }
        protected abstract TWrapper Wrap(RaycastHit hit);
        protected abstract RaycastHit Unwrap(TWrapper wrapper);
    }
}