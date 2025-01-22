using UnityEngine;

namespace PhysicsQuery
{
    public class GizmoShape_Ray : GizmoShape<RayQuery>
    {
        public GizmoShape_Ray(RayQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape()
        {
            Gizmos.DrawLine(GetStartPosition(), GetEndPosition());
        }
        protected override void DrawShape(Vector3 center)
        {
            // No shapes to draw for raycasting
        }
    }
}