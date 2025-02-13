using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class AdvancedOptions3D : AdvancedOptions
    {
        public AdvancedOptions3D(LayerMask layerMask, QueryTriggerInteraction triggerInteraction, int cacheCapacity) : base(layerMask, triggerInteraction, cacheCapacity)
        {
        }
    }
}