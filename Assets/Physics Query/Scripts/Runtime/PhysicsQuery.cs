using System;
using UnityEngine;
using Unity.Profiling;

namespace PQuery
{
    public class PhysicsQuery : MonoBehaviour
    {
        protected const float MinNonZeroFloat = 1E-5f;
        private const int MinCacheCapacity = 1;

        public static event Action<PhysicsQuery> DrawGizmos = delegate { };
        public static event Action<PhysicsQuery> DrawGizmosSelected = delegate { };

        public PhysicsShape Shape
        {
            get => _shape.Shape;
            set => _shape.SetShape(value);
        }
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

        [SerializeField]
        private PhysicsShapePair _shape;
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
        private readonly ProfilerMarker _castNonAllocMarker = new($"{nameof(PhysicsQuery)}.{nameof(CastNonAlloc)}");
        private readonly ProfilerMarker _checkMarker = new($"{nameof(PhysicsQuery)}.{nameof(Check)}");
        private readonly ProfilerMarker _overlapNonAllocMarker = new($"{nameof(PhysicsQuery)}.{nameof(OverlapNonAlloc)}");

        public bool Cast()
        {
            return Cast(out _);
        }
        public bool Cast(out RaycastHit hit)
        {
            _castMarker.Begin();
            Ray worldRay = GetWorldRay();
            bool didHit = _shape.Cast(this, worldRay, out hit);
            _castMarker.End();
            return didHit;
        }
        public Result<RaycastHit> CastNonAlloc(ResultSort sort)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }
            _castNonAllocMarker.Begin();
            Ray worldRay = GetWorldRay();
            RaycastHit[] hits = GetHitCache();
            int count = _shape.CastNonAlloc(this, worldRay, hits);
            sort.Sort(hits, count);
            _castNonAllocMarker.End();
            return new(hits, count);
        }
        public bool Check()
        {
            _checkMarker.Begin();
            Vector3 origin = GetWorldOrigin();
            bool check = _shape.Check(this, origin);
            _checkMarker.End();
            return check;
        }
        public Result<Collider> OverlapNonAlloc()
        {
            _overlapNonAllocMarker.Begin();
            Vector3 origin = GetWorldOrigin();
            Collider[] overlaps = GetColliderCache();
            int count = _shape.OverlapNonAlloc(this, origin, overlaps);
            _overlapNonAllocMarker.End();
            return new(overlaps, count);
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
        public void RefreshCache()
        {
            RefreshHitCache();
            RefreshColliderCache();
        }
        public void RefreshHitCache()
        {
            _hitCache.SetCapacity(_cacheCapacity);
        }
        public void RefreshColliderCache()
        {
            _colliderCache.SetCapacity(_cacheCapacity);
        }
        internal RaycastHit[] GetHitCache()
        {
            return _hitCache.GetArray(_cacheCapacity);
        }
        internal Collider[] GetColliderCache()
        {
            return _colliderCache.GetArray(_cacheCapacity);
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
    }
}
