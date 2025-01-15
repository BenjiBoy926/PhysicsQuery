using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Box : PreviewShape<BoxQuery>
    {
        public PreviewShape_Box(BoxQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawShape(Query.GetWorldOrigin(), color);
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