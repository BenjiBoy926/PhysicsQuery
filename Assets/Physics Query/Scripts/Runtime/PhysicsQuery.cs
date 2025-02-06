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

        public Space Space
        {
            get => _space;
            set => _space = value;
        }
        public Vector3 Start
        {
            get => _start;
            set => _start = value;
        }
        public Vector3 End
        {
            get => _end;
            set => _end = value;
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
        public PhysicsShape Shape
        {
            get => _shape;
            set => _shape = value;
        }

        [SerializeField] 
        private Space _space;
        [SerializeField] 
        private Vector3 _start = Vector3.zero;
        [SerializeField] 
        private Vector3 _end;
        [SerializeField] 
        private LayerMask _layerMask;
        [SerializeField] 
        private QueryTriggerInteraction _triggerInteraction;
        [SerializeField] 
        private int _cacheCapacity;
        [SerializeReference, SubtypeDropdown]
        private PhysicsShape _shape = new PhysicsShape_Ray();
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
            RayDistance worldRay = GetWorldRay();
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
            RayDistance worldRay = GetWorldRay();
            RaycastHit[] hits = GetHitCache();
            int count = _shape.CastNonAlloc(this, worldRay, hits);
            sort.Sort(hits, count);
            _castNonAllocMarker.End();
            return new(hits, count);
        }
        public bool Check()
        {
            _checkMarker.Begin();
            Vector3 center = GetWorldStart();
            bool check = _shape.Check(this, center);
            _checkMarker.End();
            return check;
        }
        public Result<Collider> OverlapNonAlloc()
        {
            _overlapNonAllocMarker.Begin();
            Vector3 center = GetWorldStart();
            Collider[] overlaps = GetColliderCache();
            int count = _shape.OverlapNonAlloc(this, center, overlaps);
            _overlapNonAllocMarker.End();
            return new(overlaps, count);
        }

        public RayDistance GetWorldRay()
        {
            return new(GetWorldStart(), GetWorldEnd());
        }
        public Vector3 GetWorldStart()
        {
            return _space == Space.Self ? transform.TransformPoint(_start) : _start;
        }
        public Vector3 GetWorldEnd()
        {
            return _space == Space.Self ? transform.TransformPoint(_end) : _end;
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
            _end = Settings.DefaultEnd;
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
        public void DrawOverlapGizmo()
        {
            _shape.DrawOverlapGizmo(this);
        }
        public void DrawGizmo(Vector3 center)
        {
            _shape.DrawGizmo(this, center);
        }
    }
}
