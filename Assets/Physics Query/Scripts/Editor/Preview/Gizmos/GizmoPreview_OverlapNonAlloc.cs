using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_OverlapNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            var result = query.AgnosticOverlapNonAlloc();
            Gizmos.color = Preferences.GetColorForResult(result);
            query.DrawOverlapGizmo();

            Gizmos.color = Preferences.ResultItemColor.Value;
            for (int i = 0; i < result.Count; i++)
            {
                ColliderGizmos.DrawGizmos(result[i]);
            }
        }
    }
}