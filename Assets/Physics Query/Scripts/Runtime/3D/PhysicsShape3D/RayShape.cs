using System;
using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, PhysicsQuery3D.Shape, AdvancedOptions3D>
    {
        [Serializable]
        public class RayShape : Shape
        {
            public RayShape()
            {
            }

            public override bool Cast(Parameters parameters, out RaycastHit hit)
            {
                return Physics.Raycast(
                    GetRay(parameters),
                    out hit,
                    parameters.Distance,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
            }
            public override Result<RaycastHit> CastNonAlloc(Parameters parameters)
            {
                int count = Physics.RaycastNonAlloc(
                    GetRay(parameters),
                    parameters.HitCache,
                    parameters.Distance,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
                return new(parameters.HitCache, count);
            }
            public override bool Check(Parameters parameters)
            {
                return Physics.Linecast(
                    parameters.Origin,
                    GetEnd(parameters),
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
            }
            public override Result<Collider> OverlapNonAlloc(Parameters parameters)
            {
                Result<RaycastHit> result = CastNonAlloc(parameters);
                for (int i = 0; i < result.Count; i++)
                {
                    parameters.ColliderCache[i] = result[i].collider;
                }
                return new(parameters.ColliderCache, result.Count);
            }
            public override void DrawOverlapGizmo(Parameters parameters)
            {
                Vector3 start = parameters.Origin;
                Vector3 end = parameters.Origin;
                Gizmos.DrawLine(start, end);
            }
            public override void DrawGizmo(Parameters parameters, Vector3 center)
            {
                // No shapes to draw for raycasting
            }

            private Vector3 GetEnd(Parameters parameters)
            {
                return GetRay(parameters).GetPoint(parameters.Distance);
            }
        }
    }
}