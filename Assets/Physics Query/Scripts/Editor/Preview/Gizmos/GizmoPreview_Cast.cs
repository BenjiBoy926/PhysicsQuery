using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_Cast : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery3D query)
        {
            bool result = query.Cast(out RaycastHit hit);
            VectorWrapper3D start = query.GetWorldStart();
            if (result)
            {
                DrawHit(hit);
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start.Unwrap(), GetShapeCenter(query, hit));
                query.DrawGizmo(start);
                DrawShapeAtHit(query, hit);
            }
            else
            {
                Gizmos.color = Preferences.MissColor.Value;
                VectorWrapper3D end = query.GetWorldEnd();
                query.DrawGizmo(start);
                query.DrawGizmo(end);
                Gizmos.DrawLine(start.Unwrap(), end.Unwrap());
            }
        }
    }
}