using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_Check : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            _query = query;
            bool result = query.Check();
            DrawResult(result);
        }
        private void DrawResult(bool check)
        {
            Gizmos.color = check ? Preferences.HitColor.Value : Preferences.MissColor.Value;
            Query.DrawOverlapGizmo();
        }
    }
}