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

        public override bool Cast(PhysicsParameters<Vector3, RaycastHit, Collider> parameters, out RaycastHit hit)
        {
            return Physics.Raycast(
                GetRay(parameters),
                out hit,
                parameters.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            int count = Physics.RaycastNonAlloc(
                GetRay(parameters),
                parameters.HitCache,
                parameters.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            return Physics.Linecast(
                parameters.Origin,
                GetEnd(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            Result<RaycastHit> result = CastNonAlloc(parameters);
            for (int i = 0; i < result.Count; i++)
            {
                parameters.ColliderCache[i] = result[i].collider;
            }
            return new(parameters.ColliderCache, result.Count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            Vector3 start = parameters.Origin;
            Vector3 end = parameters.Origin;
            Gizmos.DrawLine(start, end);
        }
        public override void DrawGizmo(PhysicsParameters<Vector3, RaycastHit, Collider> parameters, Vector3 center)
        {
            // No shapes to draw for raycasting
        }

        private Vector3 GetEnd(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            return GetRay(parameters).GetPoint(parameters.Distance);
        }
        private Ray GetRay(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            return new(parameters.Origin, parameters.Direction);
        }
    }
}