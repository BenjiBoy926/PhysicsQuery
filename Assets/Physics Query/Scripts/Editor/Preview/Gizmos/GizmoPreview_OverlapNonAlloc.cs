using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_OverlapNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            _query = query;
            var result = query.OverlapNonAlloc();
            DrawResult(result);
        }
        private void DrawResult(Result<Collider> result)
        {
            Gizmos.color = Preferences.GetColorForResult(result);
            Query.DrawOverlapGizmo();

            Gizmos.color = Preferences.ResultItemColor.Value;
            for (int i = 0; i < result.Count; i++)
            {
                ColliderGizmos.DrawGizmos(result[i]);
            }
        }
    }
}