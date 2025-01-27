using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoShape_Capsule : GizmoShape<CapsuleQuery>
    {
        protected override void DrawOverlapShape()
        {
            DrawShape(GetStartPosition());
        }
        protected override void DrawShape(Vector3 center)
        {
            Vector3 up = Query.GetWorldAxis();
            float radius = Query.Radius;
            CapsuleGizmo.Draw(center, up, radius);
        }
    }
}