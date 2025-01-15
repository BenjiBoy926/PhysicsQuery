using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Ray : PreviewShape<RayQuery>
    {
        public PreviewShape_Ray(RayQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawLine(Query.GetWorldOrigin(), GetEndPoint(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            // No shapes to draw for raycasting
        }
    }
}