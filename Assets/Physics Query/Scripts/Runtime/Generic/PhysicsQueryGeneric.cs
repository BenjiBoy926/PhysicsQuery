using System;
using Unity.Profiling;
using UnityEngine;

namespace PQuery
{
    public abstract partial class PhysicsQueryGeneric<TVector, TRaycastHit, TCollider, TShape, TAdvancedOptions> : PhysicsQuery
        where TCollider : Component
        where TShape : PhysicsQueryGeneric<TVector, TRaycastHit, TCollider, TShape, TAdvancedOptions>.AbstractShape
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
        public TShape CurrentShape
        {
            get => _currentShape;
            set => _currentShape = value;
        }
        public override int CacheCapacity => _advanced.CacheCapacity;
        private Func<TRaycastHit, AgnosticRaycastHit> AgnosticizeHitDelegate => _agnosticizeHitDelegate ??= Agnosticize;
        private Func<TRaycastHit, BoxedRaycastHit> BoxHitDelegate => _boxHitDelegate ??= Box;
        private Func<TCollider, Component> AgnosticizeColliderDelegate => _agnosticizeColliderDelegate ??= Agnosticize;

        [SerializeField]
        private TVector _start;
        [SerializeField]
        private TVector _end;
        [SerializeField]
        private TAdvancedOptions _advanced;
        [SerializeReference, SubtypeDropdown]
        private TShape _currentShape;
        
        private Func<TRaycastHit, AgnosticRaycastHit> _agnosticizeHitDelegate;
        private Func<TRaycastHit, BoxedRaycastHit> _boxHitDelegate;
        private Func<TCollider, Component> _agnosticizeColliderDelegate;

        private readonly CachedArray<TRaycastHit> _hitCache = new();
        private readonly CachedArray<TCollider> _colliderCache = new();

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
            bool didHit = _currentShape.Cast(GetParameters(), out hit);
            _castMarker.End();
            return didHit;
        }
        public Result<TRaycastHit> CastNonAlloc(ResultSort<TRaycastHit> sort)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }
            _castNonAllocMarker.Begin();
            Result<TRaycastHit> result = _currentShape.CastNonAlloc(GetParameters());
            sort.Execute(result.Cache, result.Count);
            _castNonAllocMarker.End();
            return result;
        }
        public override bool Check()
        {
            _checkMarker.Begin();
            bool check = _currentShape.Check(GetParameters());
            _checkMarker.End();
            return check;
        }
        public Result<TCollider> OverlapNonAlloc()
        {
            _overlapNonAllocMarker.Begin();
            Result<TCollider> result = _currentShape.OverlapNonAlloc(GetParameters());
            _overlapNonAllocMarker.End();
            return result;
        }

        public Parameters GetParameters()
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
        public override void RefreshCache()
        {
            base.RefreshCache();
            RefreshHitCache();
            RefreshColliderCache();
        }
        public void RefreshHitCache()
        {
            _hitCache.SetCapacity(CacheCapacity);
        }
        public void RefreshColliderCache()
        {
            _colliderCache.SetCapacity(CacheCapacity);
        }

        public override void DrawOverlapGizmo()
        {
            _currentShape.DrawOverlapGizmo(GetParameters());
        }
        public override void DrawGizmo(Vector3 center)
        {
            _currentShape.DrawGizmo(GetParameters(), center);
        }

        public override Vector3 GetAgnosticWorldStart()
        {
            return Unwrap(GetWorldStart());
        }
        public override Vector3 GetAgnosticWorldEnd()
        {
            return Unwrap(GetWorldEnd());
        }
        public override bool AgnosticCast(out AgnosticRaycastHit hit)
        {
            return SelectCastResult(AgnosticizeHitDelegate, out hit);
        }
        public override bool BoxedCast(out BoxedRaycastHit hit)
        {
            return SelectCastResult(BoxHitDelegate, out hit);
        }
        public override Result<AgnosticRaycastHit> AgnosticCastNonAlloc(ResultSort<AgnosticRaycastHit> resultSort)
        {
            return SelectCastNonAllocResult(resultSort, AgnosticHitCache, AgnosticizeHitDelegate);
        }
        public override Result<BoxedRaycastHit> BoxedCastNonAlloc(ResultSort<BoxedRaycastHit> resultSort)
        {
            return SelectCastNonAllocResult(resultSort, BoxedHitCache, BoxHitDelegate);
        }
        public override Result<Component> AgnosticOverlapNonAlloc()
        {
            return SelectResult(OverlapNonAlloc(), AgnosticColliderCache, AgnosticizeColliderDelegate);
        }

        private bool SelectCastResult<TNewRaycastHit>(Func<TRaycastHit, TNewRaycastHit> cast, out TNewRaycastHit result)
        {
            bool didHit = Cast(out TRaycastHit genericHit);
            result = cast(genericHit);
            return didHit;
        }
        private Result<TNewRaycastHit> SelectCastNonAllocResult<TNewRaycastHit>(ResultSort<TNewRaycastHit> resultSort, CachedArray<TNewRaycastHit> cache, Func<TRaycastHit, TNewRaycastHit> cast)
        {
            ResultSort<TRaycastHit> none = GetNoneSort();
            Result<TRaycastHit> inResult = CastNonAlloc(none);
            Result<TNewRaycastHit> outResult = SelectResult(inResult, cache, cast);
            resultSort.Execute(outResult.Cache, inResult.Count);
            return outResult;
        }
        private Result<TOut> SelectResult<TIn, TOut>(Result<TIn> inResult, CachedArray<TOut> outCache, Func<TIn, TOut> cast)
        {
            TOut[] outArray = outCache.GetArray(CacheCapacity);
            inResult.Select(outArray, cast);
            return new(outArray, inResult.Count);
        }

        private Component Agnosticize(TCollider collider)
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

        protected override void Reset()
        {
            base.Reset();
            _advanced = GetDefaultOptions();
            _currentShape = GetDefaultShape();
        }

        protected abstract TShape GetDefaultShape();
        protected abstract TAdvancedOptions GetDefaultOptions();
        protected abstract ResultSort<TRaycastHit> GetNoneSort();
        protected abstract AgnosticRaycastHit Agnosticize(TRaycastHit raycastHit);
        protected abstract BoxedRaycastHit Box(TRaycastHit raycastHit);
        protected abstract TVector Wrap(Vector3 vector);
        protected abstract Vector3 Unwrap(TVector vector);
    }
}