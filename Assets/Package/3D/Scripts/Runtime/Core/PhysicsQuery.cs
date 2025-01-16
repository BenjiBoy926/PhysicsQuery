using System;
using UnityEngine;

namespace PhysicsQuery
{
    public abstract class PhysicsQuery : MonoBehaviour
    {
        protected const float MinNonZeroFloat = 1E-5f;
        private const int MinCacheCapacity = 1;

        public Space Space
        {
            get => _space;
            set => _space = value;
        }
        public Vector3 Origin
        {
            get => _origin;
            set => _origin = value;
        }
        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }
        public float MaxDistance
        {
            get => _maxDistance;
            set => _maxDistance = Mathf.Max(value, MinNonZeroFloat);
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
        public bool IsEmpty => GetType() == typeof(EmptyQuery);

        [SerializeField] 
        private Space _space = Space.Self;
        [SerializeField] 
        private Vector3 _origin = Vector3.zero;
        [SerializeField] 
        private Vector3 _direction = Vector3.forward;
        [SerializeField] 
        private float _maxDistance = Mathf.Infinity;
        [SerializeField] 
        private LayerMask _layerMask = Physics.DefaultRaycastLayers;
        [SerializeField] 
        private QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.UseGlobal;
        [SerializeField] 
        private int _cacheCapacity = 8;
        private RaycastHit[] _hitCache;
        private Collider[] _colliderCache;
        private Preview _preview;

        public CastResult Cast(ResultSort sort)
        {
            Ray worldRay = GetWorldRay();
            RaycastHit[] hits = GetHitCache();
            int count = PerformCast(worldRay, hits);
            sort.Sort(hits, count);
            return new(hits, count);
        }
        public OverlapResult Overlap()
        {
            Vector3 origin = GetWorldOrigin();
            Collider[] overlaps = GetColliderCache();
            int count = PerformOverlap(origin, overlaps);
            return new(overlaps, count);
        }

        protected abstract int PerformCast(Ray worldRay, RaycastHit[] cache);
        protected abstract int PerformOverlap(Vector3 worldOrigin, Collider[] cache);

        internal RaycastHit[] GetHitCache()
        {
            if (CacheNeedsRebuild(_hitCache))
            {
                RebuildCache(ref _hitCache);
            }
            return _hitCache;
        }
        internal Collider[] GetColliderCache()
        {
            if (CacheNeedsRebuild(_colliderCache))
            {
                RebuildCache(ref _colliderCache);
            }
            return _colliderCache;
        }
        private bool CacheNeedsRebuild<TElement>(TElement[] cache)
        {
            return cache == null || cache.Length != _cacheCapacity;
        }
        private void RebuildCache<TElement>(ref TElement[] cache)
        {
            cache = new TElement[_cacheCapacity];
        }

        public Ray GetWorldRay()
        {
            return new(GetWorldOrigin(), GetWorldDirection());
        }
        public Vector3 GetWorldOrigin()
        {
            return _space == Space.Self ? transform.TransformPoint(_origin) : _origin;
        }
        public Vector3 GetWorldDirection()
        {
            return _space == Space.Self ? transform.TransformDirection(_direction) : _direction;
        }

        internal void SetPreview(Preview preview)
        {
            _preview = preview;
        }
        
        private void OnValidate()
        {
            _maxDistance = Mathf.Max(0, _maxDistance);
            _cacheCapacity = Mathf.Max(0, _cacheCapacity);
        }
        private void OnDrawGizmosSelected()
        {
            _preview?.DrawGizmos();
        }
    }
}
