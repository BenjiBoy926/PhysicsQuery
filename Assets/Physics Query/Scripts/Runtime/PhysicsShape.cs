using System;
using Unity.Profiling;
using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsShape
    {
        private readonly ProfilerMarker _castMarker = new($"{nameof(PhysicsQuery)}.{nameof(Cast)}");
        private readonly ProfilerMarker _castNonAllocMarker = new($"{nameof(PhysicsQuery)}.{nameof(CastNonAlloc)}");
        private readonly ProfilerMarker _checkMarker = new($"{nameof(PhysicsQuery)}.{nameof(Check)}");
        private readonly ProfilerMarker _overlapNonAllocMarker = new($"{nameof(PhysicsQuery)}.{nameof(OverlapNonAlloc)}");

        public bool Cast(PhysicsQuery query)
        {
            return Cast(query, out _);
        }
        public bool Cast(PhysicsQuery query, out RaycastHit hit)
        {
            _castMarker.Begin();
            Ray worldRay = query.GetWorldRay();
            bool didHit = DoPhysicsCast(query, worldRay, out hit);
            _castMarker.End();
            return didHit;
        }
        public Result<RaycastHit> CastNonAlloc(PhysicsQuery query, ResultSort sort)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }
            _castNonAllocMarker.Begin();
            Ray worldRay = query.GetWorldRay();
            RaycastHit[] hits = query.GetHitCache();
            int count = DoPhysicsCastNonAlloc(query, worldRay, hits);
            sort.Sort(hits, count);
            _castNonAllocMarker.End();
            return new(hits, count);
        }
        public bool Check(PhysicsQuery query)
        {
            _checkMarker.Begin();
            Vector3 origin = query.GetWorldOrigin();
            bool check = DoPhysicsCheck(query, origin);
            _checkMarker.End();
            return check;
        }
        public Result<Collider> OverlapNonAlloc(PhysicsQuery query)
        {
            _overlapNonAllocMarker.Begin();
            Vector3 origin = query.GetWorldOrigin();
            Collider[] overlaps = query.GetColliderCache();
            int count = DoPhysicsOverlapNonAlloc(query, origin, overlaps);
            _overlapNonAllocMarker.End();
            return new(overlaps, count);
        }

        protected abstract bool DoPhysicsCast(PhysicsQuery query, Ray worldRay, out RaycastHit hit);
        protected abstract int DoPhysicsCastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache);
        protected abstract bool DoPhysicsCheck(PhysicsQuery query, Vector3 worldOrigin);
        protected abstract int DoPhysicsOverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache);
    }
}