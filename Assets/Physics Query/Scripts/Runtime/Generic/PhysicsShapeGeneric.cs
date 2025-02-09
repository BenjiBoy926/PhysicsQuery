namespace PQuery
{
    public abstract class PhysicsShapeGeneric<TVector, TRay, TRaycastHit, TCollider>
        where TVector : IVector<TVector>
        where TRay : IRay<TVector>
    {
        public abstract bool Cast(PhysicsParameters<TVector, TRay, TRaycastHit, TCollider> parameters, out TRaycastHit hit);
        public abstract Result<TRaycastHit> CastNonAlloc(PhysicsParameters<TVector, TRay, TRaycastHit, TCollider> parameters);
        public abstract bool Check(PhysicsParameters<TVector, TRay, TRaycastHit, TCollider> parameters);
        public abstract Result<TCollider> OverlapNonAlloc(PhysicsParameters<TVector, TRay, TRaycastHit, TCollider> parameters);
        public abstract void DrawOverlapGizmo(PhysicsParameters<TVector, TRay, TRaycastHit, TCollider> parameters);
        public abstract void DrawGizmo(PhysicsParameters<TVector, TRay, TRaycastHit, TCollider> parameters, TVector center);
    }
}