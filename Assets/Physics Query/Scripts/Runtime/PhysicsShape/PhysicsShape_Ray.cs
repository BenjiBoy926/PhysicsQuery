using NUnit.Framework.Constraints;
using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Ray : PhysicsShape
    {
        public PhysicsShape_Ray()
        {
        }

        public override bool Cast(PhysicsParameters parameters, out RaycastHit hit)
        {
            RayDistance worldRay = parameters.GetWorldRay();
            return Physics.Raycast(
                worldRay.Ray,
                out hit,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters parameters)
        {
            RayDistance worldRay = parameters.GetWorldRay();
            int count = Physics.RaycastNonAlloc(
                worldRay.Ray,
                parameters.HitCache,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters parameters)
        {
            return Physics.Linecast(
                parameters.GetWorldStart(),
                parameters.GetWorldEnd(),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters parameters)
        {
            Result<RaycastHit> result = CastNonAlloc(parameters);
            for (int i = 0; i < result.Count; i++)
            {
                parameters.ColliderCache[i] = result[i].collider;
            }
            return new(parameters.ColliderCache, result.Count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters parameters)
        {
            Vector3 start = parameters.GetWorldStart();
            Vector3 end = parameters.GetWorldEnd();
            Gizmos.DrawLine(start, end);
        }
        public override void DrawGizmo(PhysicsParameters parameters, Vector3D center)
        {
            // No shapes to draw for raycasting
        }
    }
}