using UnityEngine;

namespace PhysicsQuery
{
    public class GizmoShape_Capsule : GizmoShape<CapsuleQuery>
    {
        public GizmoShape_Capsule(CapsuleQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawShape(GetStartPosition(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            Vector3 up = Query.GetWorldAxis();
            float radius = Query.Radius;
            Gizmos.color = color;
            CapsuleGizmo.Draw(center, up, radius);
        }
    }
}