using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_Cast : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            _query = query;
            bool result = query.Cast(out RaycastHit hit);
            DrawResult(result, hit);
        }
        private void DrawResult(bool didHit, RaycastHit hit)
        {
            Vector3 start = GetStartPosition();
            if (didHit)
            {
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start, GetShapeCenter(hit));
                Query.DrawGizmo(start);
                DrawHit(hit);
            }
            else
            {
                Gizmos.color = Preferences.MissColor.Value;
                Vector3 end = GetEndPosition();
                Query.DrawGizmo(start);
                Query.DrawGizmo(end);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}