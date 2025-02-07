using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_Check : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery3D query)
        {
            bool result = query.Check();
            Gizmos.color = result ? Preferences.HitColor.Value : Preferences.MissColor.Value;
            query.DrawOverlapGizmo();
        }
    }
}