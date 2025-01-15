using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Box : PreviewShape<BoxQuery>
    {
        public PreviewShape_Box(BoxQuery query) : base(query)
        {
        }

        public override void DrawCastGizmos()
        {
            PhysicsCastResult result = Query.Cast();
            if (result.IsEmpty)
            {
                Vector3 start = Query.GetWorldOrigin();
                Vector3 end = GetEndPoint();
                DrawShape(start, Color.gray);
                DrawShape(end, Color.gray);
                DrawDefaultLine();
            }
            else
            {
                DrawCastResults(result);
                DrawShape(GetEndPoint(), Color.gray);
            }
        }
        public override void DrawOverlapGizmos()
        {
            PhysicsOverlapResult result = Query.Overlap();
            if (result.IsEmpty)
            {
                DrawShape(Query.GetWorldOrigin(), Color.gray);
            }
            else
            {
                DrawShape(Query.GetWorldOrigin(), Color.green);
            }
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            Quaternion worldOrientation = Query.GetWorldOrientation();
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            center = rotationMatrix.inverse.MultiplyVector(center);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = color;
            Gizmos.DrawWireCube(center, Query.GetWorldSize());
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}