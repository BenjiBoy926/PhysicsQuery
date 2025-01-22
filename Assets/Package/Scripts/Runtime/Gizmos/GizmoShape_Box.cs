using UnityEngine;

namespace PhysicsQuery
{
    public class GizmoShape_Box : GizmoShape<BoxQuery>
    {
        public GizmoShape_Box(BoxQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape()
        {
            DrawShape(GetStartPosition());
        }
        protected override void DrawShape(Vector3 center)
        {
            Quaternion worldOrientation = Query.GetWorldOrientation();
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            center = rotationMatrix.inverse.MultiplyVector(center);

            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(center, Query.Size);
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}