namespace PQuery
{
    public abstract class PhysicsShapeGeneric<TVector, TRaycastHit, TCollider, TAdvancedOptions>
    {
        public abstract bool Cast(PhysicsParameters<TVector, TRaycastHit, TCollider, TAdvancedOptions> parameters, out TRaycastHit hit);
        public abstract Result<TRaycastHit> CastNonAlloc(PhysicsParameters<TVector, TRaycastHit, TCollider, TAdvancedOptions> parameters);
        public abstract bool Check(PhysicsParameters<TVector, TRaycastHit, TCollider, TAdvancedOptions> parameters);
        public abstract Result<TCollider> OverlapNonAlloc(PhysicsParameters<TVector, TRaycastHit, TCollider, TAdvancedOptions> parameters);
        public abstract void DrawOverlapGizmo(PhysicsParameters<TVector, TRaycastHit, TCollider, TAdvancedOptions> parameters);
        public abstract void DrawGizmo(PhysicsParameters<TVector, TRaycastHit, TCollider, TAdvancedOptions> parameters, TVector center);
    }
}