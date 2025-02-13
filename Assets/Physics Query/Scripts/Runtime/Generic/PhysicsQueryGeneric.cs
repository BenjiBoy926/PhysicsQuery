using System;
using Unity.Profiling;
using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsQueryGeneric<TVector, TRaycastHit, TCollider, TResultSort, TPhysicsShape, TAdvancedOptions> : PhysicsQuery
        where TCollider : Component
        where TResultSort : ResultSortGeneric<TRaycastHit>
        where TPhysicsShape : PhysicsShapeGeneric<TVector, TRaycastHit, TCollider, TAdvancedOptions>
        where TAdvancedOptions : AdvancedOptions
    {
        public TVector Start
        {
            get => _start;
            set => _start = value;
        }
        public TVector End
        {
            get => _end;
            set => _end = value;
        }
        public TAdvancedOptions Advanced
        {
            get => _advanced;
            set => _advanced = value;
        }
        public TPhysicsShape Shape
        {
            get => _shape;
            set => _shape = value;
        }
        private int CacheCapacity => _advanced.CacheCapacity;
        private Func<TRaycastHit, MinimalRaycastHit> MinimizeRaycastHitDelegate => _minimizeRaycastHitDelegate ??= MinimizeRaycastHit;
        private Func<TCollider, Component> MinimizeColliderDelegate => _minimizeColliderDelegate ??= MinimizeCollider;

        [Space]
        [SerializeField]
        private TVector _start;
        [SerializeField]
        private TVector _end;
        [SerializeField]
        private TAdvancedOptions _advanced;
        [SerializeReference, SubtypeDropdown]
        private TPhysicsShape _shape;
        
        private Func<TRaycastHit, MinimalRaycastHit> _minimizeRaycastHitDelegate;
        private Func<TCollider, Component> _minimizeColliderDelegate;

        private readonly CachedArray<TRaycastHit> _hitCache = new();
        private readonly CachedArray<TCollider> _colliderCache = new();
        private readonly CachedArray<MinimalRaycastHit> _minimalHitCache = new();
        private readonly CachedArray<Component> _minimalColliderCache = new();

        private readonly ProfilerMarker _castMarker = new($"{nameof(PhysicsQuery3D)}.{nameof(Cast)}");
        private readonly ProfilerMarker _castNonAllocMarker = new($"{nameof(PhysicsQuery3D)}.{nameof(CastNonAlloc)}");
        private readonly ProfilerMarker _checkMarker = new($"{nameof(PhysicsQuery3D)}.{nameof(Check)}");
        private readonly ProfilerMarker _overlapNonAllocMarker = new($"{nameof(PhysicsQuery3D)}.{nameof(OverlapNonAlloc)}");

        public bool Cast()
        {
            return Cast(out _);
        }
        public bool Cast(out TRaycastHit hit)
        {
            _castMarker.Begin();
            bool didHit = _shape.Cast(GetParameters(), out hit);
            _castMarker.End();
            return didHit;
        }
        public Result<TRaycastHit> CastNonAlloc(TResultSort sort)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }
            _castNonAllocMarker.Begin();
            Result<TRaycastHit> result = _shape.CastNonAlloc(GetParameters());
            sort.Sort(result._cache, result.Count);
            _castNonAllocMarker.End();
            return result;
        }
        public override bool Check()
        {
            _checkMarker.Begin();
            bool check = _shape.Check(GetParameters());
            _checkMarker.End();
            return check;
        }
        public Result<TCollider> OverlapNonAlloc()
        {
            _overlapNonAllocMarker.Begin();
            Result<TCollider> result = _shape.OverlapNonAlloc(GetParameters());
            _overlapNonAllocMarker.End();
            return result;
        }

        public PhysicsParameters<TVector, TRaycastHit, TCollider, TAdvancedOptions> GetParameters()
        {
            Matrix4x4 matrix = GetTransformationMatrix();
            TVector origin = GetWorldStart();
            TVector end = GetWorldEnd();

            Vector3 startToEnd = Unwrap(end) - Unwrap(origin);
            Vector3 direction = startToEnd.normalized;
            float distance = startToEnd.magnitude;
            
            return new(
                matrix,
                origin,
                Wrap(direction),
                distance,
                _hitCache.GetArray(CacheCapacity),
                _colliderCache.GetArray(CacheCapacity),
                _advanced);
        }
        public TVector GetWorldStart()
        {
            return TransformPoint(_start);
        }
        public TVector GetWorldEnd()
        {
            return TransformPoint(_end);
        }
        public void RefreshCache()
        {
            RefreshHitCache();
            RefreshColliderCache();
            RefreshMinimalHitCache();
            RefreshMinimalColliderCache();
        }
        public void RefreshHitCache()
        {
            _hitCache.SetCapacity(CacheCapacity);
        }
        public void RefreshColliderCache()
        {
            _colliderCache.SetCapacity(CacheCapacity);
        }
        public void RefreshMinimalHitCache()
        {
            _minimalHitCache.SetCapacity(CacheCapacity);
        }
        public void RefreshMinimalColliderCache()
        {
            _minimalColliderCache.SetCapacity(CacheCapacity);
        }

        public void DrawOverlapGizmo()
        {
            _shape.DrawOverlapGizmo(GetParameters());
        }
        public void DrawGizmo(TVector center)
        {
            _shape.DrawGizmo(GetParameters(), center);
        }

        public override bool MinimalCast(out MinimalRaycastHit hit)
        {
            bool didHit = Cast(out TRaycastHit genericHit);
            hit = MinimizeRaycastHit(genericHit);
            return didHit;
        }
        public override Result<MinimalRaycastHit> MinimalCastNonAlloc(ResultSortMinimal resultSort)
        {
            TResultSort none = GetNoneSort();
            Result<TRaycastHit> result = CastNonAlloc(none);
            MinimalRaycastHit[] cache = _minimalHitCache.GetArray(CacheCapacity);
            result.CopyTo(cache, MinimizeRaycastHitDelegate);
            resultSort.Sort(cache, result.Count);
            return new(cache, result.Count);
        }
        public override Result<Component> MinimalOverlapNonAlloc()
        {
            Component[] cache = _minimalColliderCache.GetArray(CacheCapacity);
            Result<TCollider> result = OverlapNonAlloc();
            result.CopyTo(cache, MinimizeColliderDelegate);
            return new(cache, result.Count);
        }

        private Component MinimizeCollider(TCollider collider)
        {
            return collider;
        }
        private TVector TransformPoint(TVector point)
        {
            Vector3 result = Unwrap(point);
            if (Space == Space.Self)
            {
                result = transform.TransformPoint(result);
            }
            return Wrap(result);
        }

        protected abstract TResultSort GetNoneSort();
        protected abstract MinimalRaycastHit MinimizeRaycastHit(TRaycastHit raycastHit);
        protected abstract TVector Wrap(Vector3 vector);
        protected abstract Vector3 Unwrap(TVector vector);
    }
}