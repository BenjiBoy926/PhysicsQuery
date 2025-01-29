using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoShape_Box : GizmoShape
    {
        protected override void DrawOverlapShape()
        {
            DrawShape(GetStartPosition());
        }
        protected override void DrawShape(Vector3 center)
        {
            PhysicsShape_Box shape = (PhysicsShape_Box)Query.Shape;
            Quaternion worldOrientation = shape.GetWorldOrientation(Query);
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            center = rotationMatrix.inverse.MultiplyVector(center);

            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(center, shape.Size);
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}