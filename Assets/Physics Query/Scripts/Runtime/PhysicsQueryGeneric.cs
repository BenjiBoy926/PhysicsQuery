using System;
using Unity.Profiling;
using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsQueryGeneric<TVector, TRay, TRaycastHit, TCollider, TPhysicsParameters, TRayDistance, TResultSort, TPhysicsShape> : PhysicsQuery
        where TVector : IVector<TVector>
        where TRay : IRay<TVector>
        where TPhysicsParameters : PhysicsParametersGeneric<TVector, TRay, TRayDistance, TRaycastHit, TCollider>, new()
        where TRayDistance : RayDistanceGeneric<TVector, TRay>, new()
        where TResultSort : ResultSortGeneric<TRaycastHit>
        where TPhysicsShape : PhysicsShapeGeneric<TVector, TCollider, TRaycastHit, TPhysicsParameters>
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
        private readonly TPhysicsParameters _parameters = new();
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
        public bool Check()
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

        public TPhysicsParameters GetParameters()
        {
            _parameters.Space = GetTransformationMatrix();
            _parameters.LayerMask = LayerMask;
            _parameters.TriggerInteraction = TriggerInteraction;
            _parameters.Start = Start;
            _parameters.End = End;
            _parameters.HitCache = GetHitCache();
            _parameters.ColliderCache = GetColliderCache();
            return _parameters;
        }
        public TRayDistance GetWorldRay()
        {
            return (TRayDistance)GetParameters().GetWorldRay();
        }
        public TVector GetWorldStart()
        {
            return _start.TransformAsPoint(GetTransformationMatrix());
        }
        public TVector GetWorldEnd()
        {
            return _end.TransformAsPoint(GetTransformationMatrix());
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
    }
}