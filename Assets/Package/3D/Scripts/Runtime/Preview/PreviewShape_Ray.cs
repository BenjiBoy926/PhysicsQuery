using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Ray : PreviewShape<RayQuery>
    {
        public PreviewShape_Ray(RayQuery query) : base(query)
        {
        }

        public override void DrawOverlapGizmos()
        {
            PhysicsOverlapResult result = Query.Overlap();
            if (result.IsEmpty)
            {
                DrawDefaultLine();
            }
            else
            {
                // TODO: draw an outline of the colliders
                DrawDefaultLine(Color.green);
            }
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            // No shapes to draw for raycasting
        }
    }
}