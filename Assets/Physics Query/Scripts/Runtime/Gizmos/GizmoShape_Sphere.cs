using UnityEngine;

namespace PhysicsQuery
{
    public class GizmoShape_Sphere : GizmoShape<SphereQuery>
    {
        public GizmoShape_Sphere(SphereQuery query) : base(query)
        {
        }

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