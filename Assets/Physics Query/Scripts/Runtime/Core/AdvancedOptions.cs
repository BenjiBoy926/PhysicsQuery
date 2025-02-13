using System;
using UnityEngine;

namespace PQuery
{
    public class AdvancedOptions
    {
        public LayerMask LayerMask => _layerMask;
        public QueryTriggerInteraction TriggerInteraction => _triggerInteraction;
        public int CacheCapacity => _cacheCapacity;

        [SerializeField]
        private LayerMask _layerMask;
        [SerializeField]
        private QueryTriggerInteraction _triggerInteraction;
        [SerializeField]
        private int _cacheCapacity;

        public AdvancedOptions(LayerMask layerMask, QueryTriggerInteraction triggerInteraction, int cacheCapacity)
        {
            _layerMask = layerMask;
            _triggerInteraction = triggerInteraction;
            _cacheCapacity = cacheCapacity;
        }
    }
}