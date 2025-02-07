using UnityEngine;

namespace PQuery
{
    public interface IPhysicsShape<TPhysicsParameters, TRaycastHit, TCollider, TVector>
    {
        bool Cast(TPhysicsParameters parameters, out TRaycastHit hit);
        Result<TRaycastHit> CastNonAlloc(TPhysicsParameters parameters);
        bool Check(TPhysicsParameters parameters);
        Result<TCollider> OverlapNonAlloc(TPhysicsParameters parameters);
        void DrawOverlapGizmo(TPhysicsParameters parameters);
        void DrawGizmo(TPhysicsParameters parameters, TVector center);

    }
}