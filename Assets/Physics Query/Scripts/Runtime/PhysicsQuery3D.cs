using System;
using UnityEngine;
using Unity.Profiling;

namespace PQuery
{
    public class PhysicsQuery3D : PhysicsQuery
    {
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
        public PhysicsShape Shape
        {
            get => _shape;
            set => _shape = value;
        }

        [Space]
        [SerializeField] 
        private Vector3 _start = Vector3.zero;
        [SerializeField] 
        private Vector3 _end;
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
        internal RaycastHit[] GetHitCache()
        {
            return _hitCache.GetArray(CacheCapacity);
        }
        internal Collider[] GetColliderCache()
        {
            return _colliderCache.GetArray(CacheCapacity);
        }

        protected override void Reset()
        {
            _end = Settings.DefaultEnd;
        }
        public override void DrawOverlapGizmo()
        {
            _shape.DrawOverlapGizmo(GetParameters());
        }
        public override void DrawGizmo(Vector3 center)
        {
            _shape.DrawGizmo(GetParameters(), center);
        }
    }
}
