using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class AdvancedOptions2D : AdvancedOptions
    {
        public ContactFilter2D Filter => new()
        {
            useLayerMask = true,
            layerMask = LayerMask,
            useTriggers = TriggerInteraction == QueryTriggerInteraction.UseGlobal ? 
                Physics2D.queriesHitTriggers : 
                TriggerInteraction == QueryTriggerInteraction.Collide,
            
            useDepth = true,
            useOutsideDepth = _isDepthFilterFlipped,
            minDepth = _minDepth,
            maxDepth = _maxDepth,

            useNormalAngle = true,
            useOutsideNormalAngle = _isNormalAngleFilterFlipped,
            minNormalAngle = _minNormalAngle,
            maxNormalAngle = _maxNormalAngle
        };
        public float MinDepth => _minDepth;
        public float MaxDepth => _maxDepth;

        [SerializeField]
        private bool _isDepthFilterFlipped;
        [SerializeField]
        private float _minDepth = float.MinValue;
        [SerializeField]
        private float _maxDepth = float.MaxValue;
        [SerializeField]
        private bool _isNormalAngleFilterFlipped;
        [SerializeField]
        private float _minNormalAngle = 0;
        [SerializeField]
        private float _maxNormalAngle = 360;

        public AdvancedOptions2D(
            LayerMask layerMask,
            QueryTriggerInteraction triggerInteraction,
            int cacheCapacity) : base(layerMask, triggerInteraction, cacheCapacity)
        {

        }
        public AdvancedOptions2D(
            LayerMask layerMask,
            QueryTriggerInteraction triggerInteraction,
            int cacheCapacity,
            bool isDepthFilterFlipped,
            float minDepth,
            float maxDepth,
            bool isNormalAngleFilterFlipped,
            float minNormalAngle,
            float maxNormalAngle) : base(layerMask, triggerInteraction, cacheCapacity)
        {
            _isDepthFilterFlipped = isDepthFilterFlipped;
            _minDepth = minDepth;
            _maxDepth = maxDepth;
            _isNormalAngleFilterFlipped = isNormalAngleFilterFlipped;
            _minNormalAngle = minNormalAngle;
            _maxNormalAngle = maxNormalAngle;
        }
    }
}