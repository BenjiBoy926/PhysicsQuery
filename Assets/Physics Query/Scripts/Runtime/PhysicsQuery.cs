using System;
using UnityEngine;

namespace PQuery
{
    public class PhysicsQuery : MonoBehaviour
    {
        protected const float MinNonZeroFloat = 1E-5f;
        private const int MinCacheCapacity = 1;

        public static event Action<PhysicsQuery> DrawGizmos = delegate { };
        public static event Action<PhysicsQuery> DrawGizmosSelected = delegate { };

        public Space Space
        {
            get => _space;
            set => _space = value;
        }
        public LayerMask LayerMask
        {
            get => _layerMask;
            set => _layerMask = value;
        }
        public QueryTriggerInteraction TriggerInteraction
        {
            get => _triggerInteraction;
            set => _triggerInteraction = value;
        }
        public int CacheCapacity
        {
            get => _cacheCapacity;
            set => _cacheCapacity = Mathf.Max(value, MinCacheCapacity);
        }

        [SerializeField] 
        private Space _space;
        [SerializeField] 
        private LayerMask _layerMask;
        [SerializeField] 
        private QueryTriggerInteraction _triggerInteraction;
        [SerializeField] 
        private int _cacheCapacity;

        public Matrix4x4 GetTransformationMatrix()
        {
            return _space == Space.World ? Matrix4x4.identity : transform.localToWorldMatrix;
        }

        protected virtual void Reset()
        {
            _space = Settings.DefaultQuerySpace;
            _layerMask = Settings.DefaultLayerMask;
            _triggerInteraction = Settings.DefaultTriggerInteraction;
            _cacheCapacity = Settings.DefaultCacheCapacity;
        }
        protected virtual void OnValidate()
        {
            _cacheCapacity = Mathf.Max(0, _cacheCapacity);
        }
        private void OnDrawGizmos()
        {
            DrawGizmos(this);
        }
        private void OnDrawGizmosSelected()
        {
            DrawGizmosSelected(this);
        }
    }
}
