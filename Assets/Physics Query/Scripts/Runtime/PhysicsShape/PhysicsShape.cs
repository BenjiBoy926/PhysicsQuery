using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsShape
    {
        public abstract bool Cast(PhysicsQuery query, RayDistance worldRay, out RaycastHit hit);
        public abstract int CastNonAlloc(PhysicsQuery query, RayDistance worldRay, RaycastHit[] cache);
        public abstract bool Check(PhysicsQuery query, Vector3 worldOrigin);
        public abstract int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache);
        public abstract void DrawOverlapGizmo(PhysicsQuery query);
        public abstract void DrawGizmo(PhysicsQuery query, Vector3 center);
    }
}