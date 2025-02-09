using System.Collections;
using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsShapeGeneric<TVector, TCollider, TRaycastHit, TPhysicsParameters>
        where TVector : IVector<TVector>
    {
        public abstract bool Cast(TPhysicsParameters parameters, out TRaycastHit hit);
        public abstract Result<TRaycastHit> CastNonAlloc(TPhysicsParameters parameters);
        public abstract bool Check(TPhysicsParameters parameters);
        public abstract Result<TCollider> OverlapNonAlloc(TPhysicsParameters parameters);
        public abstract void DrawOverlapGizmo(TPhysicsParameters parameters);
        public abstract void DrawGizmo(TPhysicsParameters parameters, TVector center);
    }
}