using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class AdvancedOptions3D : AdvancedOptions
    {
        public static readonly AdvancedOptions3D Default = new(
            Settings.DefaultLayerMask,
            Settings.DefaultTriggerInteraction,
            Settings.DefaultCacheCapacity);

        public AdvancedOptions3D(LayerMask layerMask, QueryTriggerInteraction triggerInteraction, int cacheCapacity) : base(layerMask, triggerInteraction, cacheCapacity)
        {
        }
    }
}