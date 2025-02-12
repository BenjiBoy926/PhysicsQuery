using System;
using Unity.Profiling;
using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsQueryGeneric<TVector, TRaycastHit, TCollider, TResultSort, TPhysicsShape> : PhysicsQuery
        where TCollider : Component
        where TResultSort : ResultSortGeneric<TRaycastHit>
        where TPhysicsShape : PhysicsShapeGeneric<TVector, TRaycastHit, TCollider>
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
        public TPhysicsShape Shape
        {
            get => _shape;
            set => _shape = value;
        }

        [Space]
        [SerializeField]
        private TVector _start;
        [SerializeField]
        private TVector _end;
        [SerializeReference, SubtypeDropdown]
        private TPhysicsShape _shape;
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

        public PhysicsParameters<TVector, TRaycastHit, TCollider> GetParameters()
        {
            Matrix4x4 matrix = GetTransformationMatrix();
            TVector origin = GetWorldStart();
            TVector startToEnd = Subtract(GetWorldEnd(), origin);
            TVector direction = Normalize(startToEnd);
            float distance = Magnitude(startToEnd);
            return new(matrix, LayerMask, TriggerInteraction, origin, direction, distance, GetHitCache(), GetColliderCache());
        }
        public TVector GetWorldStart()
        {
            return Space == Space.Self ? TransformAsPoint(_start) : _start;
        }
        public TVector GetWorldEnd()
        {
            return Space == Space.Self ? TransformAsPoint(_end) : _end;
        }
        public void RefreshCache()
        {
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
        internal TRaycastHit[] GetHitCache()
        {
            return _hitCache.GetArray(CacheCapacity);
        }
        internal TCollider[] GetColliderCache()
        {
            return _colliderCache.GetArray(CacheCapacity);
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
            hit = Minimize(genericHit);
            return didHit;
        }
        public override Result<MinimalRaycastHit> MinimalCastNonAlloc(ResultSortType sortType)
        {
            return CastNonAlloc(GetSort(sortType)).Select(Minimize);
        }
        public override Result<Component> MinimalOverlapNonAlloc()
        {
            return OverlapNonAlloc().Select(ColliderAsComponent);
        }

        public Component ColliderAsComponent(TCollider collider)
        {
            return collider;
        }

        public abstract TResultSort GetSort(ResultSortType sortType);
        public abstract MinimalRaycastHit Minimize(TRaycastHit raycastHit);
        public abstract float Magnitude(TVector vector);
        public abstract TVector Normalize(TVector vector);
        public abstract TVector Subtract(TVector minuend, TVector subtrahend);
        public abstract TVector TransformAsPoint(TVector point);
    }
}