using System;
using UnityEngine;
using Unity.Profiling;

namespace PQuery
{
    public class PhysicsQuery3D : MonoBehaviour
    {
        protected const float MinNonZeroFloat = 1E-5f;
        private const int MinCacheCapacity = 1;

        public static event Action<PhysicsQuery3D> DrawGizmos = delegate { };
        public static event Action<PhysicsQuery3D> DrawGizmosSelected = delegate { };

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

        private readonly ProfilerMarker _castMarker = new($"{nameof(PhysicsQuery3D)}.{nameof(Cast)}");
        private readonly ProfilerMarker _castNonAllocMarker = new($"{nameof(PhysicsQuery3D)}.{nameof(CastNonAlloc)}");
        private readonly ProfilerMarker _checkMarker = new($"{nameof(PhysicsQuery3D)}.{nameof(Check)}");
        private readonly ProfilerMarker _overlapNonAllocMarker = new($"{nameof(PhysicsQuery3D)}.{nameof(OverlapNonAlloc)}");

        public bool Cast()
        {
            return Cast(out _);
        }
        public bool Cast(out RaycastHit hit)
        {
            _castMarker.Begin();
            bool didHit = _shape.Cast(GetParameters(), out hit);
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
            Result<RaycastHit> result = _shape.CastNonAlloc(GetParameters());
            sort.Sort(result._cache, result.Count);
            _castNonAllocMarker.End();
            return result;
        }
        public bool Check()
        {
            _checkMarker.Begin();
            bool check = _shape.Check(GetParameters());
            _checkMarker.End();
            return check;
        }
        public Result<Collider> OverlapNonAlloc()
        {
            _overlapNonAllocMarker.Begin();
            Result<Collider> result = _shape.OverlapNonAlloc(GetParameters());
            _overlapNonAllocMarker.End();
            return result;
        }

        public PhysicsParameters GetParameters()
        {
            return PhysicsParameters.Snapshot(this);
        }
        public RayDistance GetWorldRay()
        {
            return new(GetWorldStart(), GetWorldEnd());
        }
        public Vector3 GetWorldStart()
        {
            return GetTransformationMatrix().MultiplyPoint3x4(_start);
        }
        public Vector3 GetWorldEnd()
        {
            return GetTransformationMatrix().MultiplyPoint3x4(_end);
        }
        public Matrix4x4 GetTransformationMatrix()
        {
            return _space == Space.World ? Matrix4x4.identity : transform.localToWorldMatrix;
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
            _shape.DrawOverlapGizmo(GetParameters());
        }
        public void DrawGizmo(Vector3 center)
        {
            _shape.DrawGizmo(GetParameters(), center);
        }
    }
}
