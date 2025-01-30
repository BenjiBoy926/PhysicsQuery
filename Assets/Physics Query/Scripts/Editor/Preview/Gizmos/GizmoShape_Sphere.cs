using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoShape_Sphere : GizmoShape
    {
        protected override void DrawOverlapShape()
        {
            DrawShape(GetStartPosition());
        }
        protected override void DrawShape(Vector3 center)
        {
            PhysicsShape_Sphere shape = (PhysicsShape_Sphere)Query.Shape;
            Gizmos.DrawWireSphere(center, shape.Radius);
        }
    }
}