using UnityEngine;

namespace PhysicsQuery
{
    public class GizmoShape_Ray : GizmoShape<RayQuery>
    {
        public GizmoShape_Ray(RayQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawLine(GetStartPosition(), GetEndPosition(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            // No shapes to draw for raycasting
        }
    }
}