using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_Cast : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery3D query)
        {
            bool result = query.Cast(out RaycastHit hit);
            Vector3 start = query.GetWorldStart();
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
                Vector3 end = query.GetWorldEnd();
                query.DrawGizmo(start);
                query.DrawGizmo(end);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}