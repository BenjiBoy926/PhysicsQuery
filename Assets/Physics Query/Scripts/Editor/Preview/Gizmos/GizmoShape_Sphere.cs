using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class GizmoShape_Sphere : GizmoShape<SphereQuery>
    {
        protected override void DrawOverlapShape()
        {
            DrawShape(GetStartPosition());
        }
        protected override void DrawShape(Vector3 center)
        {
            Gizmos.DrawWireSphere(center, Query.Radius);
        }
    }
}