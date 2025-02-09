using NUnit.Framework.Constraints;
using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape3D_Ray : PhysicsShape3D
    {
        public PhysicsShape3D_Ray()
        {
        }

        public override bool Cast(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters, out RaycastHit hit)
        {
            RayDistance<VectorWrapper3D, RayWrapper3D> worldRay = parameters.GetWorldRay();
            return Physics.Raycast(
                worldRay.Ray.Unwrap(),
                out hit,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            RayDistance<VectorWrapper3D, RayWrapper3D> worldRay = parameters.GetWorldRay();
            int count = Physics.RaycastNonAlloc(
                worldRay.Ray.Unwrap(),
                parameters.HitCache,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            return Physics.Linecast(
                parameters.GetWorldStart().Unwrap(),
                parameters.GetWorldEnd().Unwrap(),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            Result<RaycastHit> result = CastNonAlloc(parameters);
            for (int i = 0; i < result.Count; i++)
            {
                parameters.ColliderCache[i] = result[i].collider;
            }
            return new(parameters.ColliderCache, result.Count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            Vector3 start = parameters.GetWorldStart().Unwrap();
            Vector3 end = parameters.GetWorldEnd().Unwrap();
            Gizmos.DrawLine(start, end);
        }
        public override void DrawGizmo(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters, VectorWrapper3D center)
        {
            // No shapes to draw for raycasting
        }
    }
}