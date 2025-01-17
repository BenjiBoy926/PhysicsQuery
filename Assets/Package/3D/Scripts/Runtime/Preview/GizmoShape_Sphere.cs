using UnityEngine;

namespace PhysicsQuery
{
    public class GizmoShape_Sphere : GizmoShape<SphereQuery>
    {
        public GizmoShape_Sphere(SphereQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawShape(GetStartPosition(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            UnityEngine.Gizmos.color = color;
            UnityEngine.Gizmos.DrawWireSphere(center, Query.Radius);
        }
    }
}