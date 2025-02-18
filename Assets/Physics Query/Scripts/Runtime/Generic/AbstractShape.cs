using UnityEngine;

namespace PQuery
{
    public abstract partial class PhysicsQueryGeneric<TVector, TRaycastHit, TCollider, TShape, TAdvancedOptions> : PhysicsQuery
        where TCollider : Component
        where TShape : PhysicsQueryGeneric<TVector, TRaycastHit, TCollider, TShape, TAdvancedOptions>.AbstractShape
        where TAdvancedOptions : AdvancedOptions
    {
        public abstract class AbstractShape
        {
            public abstract bool Cast(Parameters parameters, out TRaycastHit hit);
            public abstract Result<TRaycastHit> CastNonAlloc(Parameters parameters);
            public abstract bool Check(Parameters parameters);
            public abstract Result<TCollider> OverlapNonAlloc(Parameters parameters);
            public abstract void DrawOverlapGizmo(Parameters parameters);
            public abstract void DrawGizmo(Parameters parameters, Vector3 center);
        }
    }
}