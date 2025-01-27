using System;
using UnityEngine;
using Unity.Profiling;

namespace PQuery
{
    public abstract class PhysicsQuery : MonoBehaviour
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
        private Space _space;
        [SerializeField] 
        private Vector3 _origin = Vector3.zero;
        [SerializeField] 
        private Vector3 _direction = Vector3.forward;
        [SerializeField] 
        private float _maxDistance;
        [SerializeField] 
        private LayerMask _layerMask;
        [SerializeField] 
        private QueryTriggerInteraction _triggerInteraction;
        [SerializeField] 
        private int _cacheCapacity;
        private readonly CachedArray<RaycastHit> _hitCache = new();
        private readonly CachedArray<Collider> _colliderCache = new();

        private readonly ProfilerMarker _castMarker = new($"{nameof(PhysicsQuery)}.{nameof(Cast)}");
        private readonly ProfilerMarker _worldRayMarker = new(nameof(GetWorldRay));
        private readonly ProfilerMarker _hitCacheMarker = new(nameof(GetHitCache));
        private readonly ProfilerMarker _doCastMarker = new(nameof(DoPhysicsCast));
        private readonly ProfilerMarker _sortMarker = new(nameof(ResultSort.Sort));

        private readonly ProfilerMarker _overlapMarker = new($"{nameof(PhysicsQuery)}.{nameof(Overlap)}");
        private readonly ProfilerMarker _worldOriginMarker = new(nameof(GetWorldOrigin));
        private readonly ProfilerMarker _colliderCacheMarker = new(nameof(GetColliderCache));
        private readonly ProfilerMarker _doOverlapMarker = new(nameof(DoPhysicsOverlap));

        public Result<RaycastHit> Cast(ResultSort sort)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }
            _castMarker.Begin(this);
            
            _worldRayMarker.Begin(this);
            Ray worldRay = GetWorldRay();
            _worldRayMarker.End();
            
            _hitCacheMarker.Begin(this);
            RaycastHit[] hits = GetHitCache();
            _hitCacheMarker.End();

            _doCastMarker.Begin(this);
            int count = DoPhysicsCast(worldRay, hits);
            _doCastMarker.End();

            _sortMarker.Begin(this);
            sort.Sort(hits, count);
            _sortMarker.End();

            _castMarker.End();
            return new(hits, count);
        }
        public Result<Collider> Overlap()
        {
            _overlapMarker.Begin(this);

            _worldOriginMarker.Begin(this);
            Vector3 origin = GetWorldOrigin();
            _worldOriginMarker.End();

            _colliderCacheMarker.Begin(this);
            Collider[] overlaps = GetColliderCache();
            _colliderCacheMarker.End();

            _doOverlapMarker.Begin(this);
            int count = DoPhysicsOverlap(origin, overlaps);
            _doOverlapMarker.End();

            _overlapMarker.End();
            return new(overlaps, count);
        }

        protected RaycastHit[] GetHitCache()
        {
            return _hitCache.GetArray(_cacheCapacity);
        }
        private Collider[] GetColliderCache()
        {
            return _colliderCache.GetArray(_cacheCapacity);
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

        protected virtual void Reset()
        {
            _space = Settings.DefaultQuerySpace;
            _maxDistance = Settings.DefaultMaxDistance;
            _layerMask = Settings.DefaultLayerMask;
            _triggerInteraction = Settings.DefaultTriggerInteraction;
            _cacheCapacity = Settings.DefaultCacheCapacity;
        }
        protected virtual void OnValidate()
        {
            _maxDistance = Mathf.Max(0, _maxDistance);
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

        protected abstract int DoPhysicsCast(Ray worldRay, RaycastHit[] cache);
        protected abstract int DoPhysicsOverlap(Vector3 worldOrigin, Collider[] cache);
    }
}
