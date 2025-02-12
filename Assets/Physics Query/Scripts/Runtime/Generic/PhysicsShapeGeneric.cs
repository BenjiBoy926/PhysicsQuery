namespace PQuery
{
    public abstract class PhysicsShapeGeneric<TVector, TRaycastHit, TCollider>
    {
        public abstract bool Cast(PhysicsParameters<TVector, TRaycastHit, TCollider> parameters, out TRaycastHit hit);
        public abstract Result<TRaycastHit> CastNonAlloc(PhysicsParameters<TVector, TRaycastHit, TCollider> parameters);
        public abstract bool Check(PhysicsParameters<TVector, TRaycastHit, TCollider> parameters);
        public abstract Result<TCollider> OverlapNonAlloc(PhysicsParameters<TVector, TRaycastHit, TCollider> parameters);
        public abstract void DrawOverlapGizmo(PhysicsParameters<TVector, TRaycastHit, TCollider> parameters);
        public abstract void DrawGizmo(PhysicsParameters<TVector, TRaycastHit, TCollider> parameters, TVector center);
    }
}