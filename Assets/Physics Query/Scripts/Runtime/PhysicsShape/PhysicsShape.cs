using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsShape
    {
        public abstract bool Cast(PhysicsParameters parameters, out RaycastHit hit);
        public abstract Result<RaycastHit> CastNonAlloc(PhysicsParameters parameters);
        public abstract bool Check(PhysicsParameters parameters);
        public abstract Result<Collider> OverlapNonAlloc(PhysicsParameters parameters);
        public abstract void DrawOverlapGizmo(PhysicsParameters parameters);
        public abstract void DrawGizmo(PhysicsParameters parameters, Vector3 center);
    }
}