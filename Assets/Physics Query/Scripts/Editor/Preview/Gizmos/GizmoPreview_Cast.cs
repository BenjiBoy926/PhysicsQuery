using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_Cast : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            bool result = query.AgnosticCast(out AgnosticRaycastHit hit);
            Vector3 start = query.GetAgnosticWorldStart();
            if (result)
            {
                DrawHit(hit);
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start, GetShapeCenter(query, hit));
                query.DrawGizmo(start);
                DrawShapeAtHit(query, hit);
            }
            else
            {
                Gizmos.color = Preferences.MissColor.Value;
                Vector3 end = query.GetAgnosticWorldEnd();
                query.DrawGizmo(start);
                query.DrawGizmo(end);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}