using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoShape_Capsule : GizmoShape
    {
        protected override void DrawOverlapShape()
        {
            DrawShape(GetStartPosition());
        }
        protected override void DrawShape(Vector3 center)
        {
            PhysicsShape_Capsule shape = (PhysicsShape_Capsule)Query.Shape;
            Vector3 up = shape.GetWorldAxis(Query);
            float radius = shape.Radius;
            CapsuleGizmo.Draw(center, up, radius);
        }
    }
}