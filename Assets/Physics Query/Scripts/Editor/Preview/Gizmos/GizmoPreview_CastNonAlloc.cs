using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_CastNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            _query = query;
            var result = query.CastNonAlloc(ResultSort.Distance);
            DrawResult(result);
        }
        private void DrawResult(Result<RaycastHit> result)
        {
            Color hitShapeColor = result.IsFull ? Preferences.CacheFullColor.Value : Preferences.HitColor.Value;

            Gizmos.color = Preferences.GetColorForResult(result);
            Query.DrawGizmo(GetStartPosition());
            for (int i = 0; i < result.Count; i++)
            {
                RaycastHit hit = result[i];
                Gizmos.color = hitShapeColor;
                DrawHit(hit);
            }
            DrawResultLine(result);

            Gizmos.color = Preferences.MissColor.Value;
            Query.DrawGizmo(GetEndPosition());
        }
        private void DrawResultLine(Result<RaycastHit> result)
        {
            Vector3 start = GetStartPosition();
            Vector3 end = GetEndPosition();

            if (result.IsFull)
            {
                Gizmos.color = Preferences.CacheFullColor.Value;
                Gizmos.DrawLine(start, end);
            }
            else if (result.IsEmpty)
            {
                Gizmos.color = Preferences.MissColor.Value;
                Gizmos.DrawLine(start, end);
            }
            else
            {
                Vector3 midpoint = GetWorldRay().GetPoint(result.Last.distance);
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start, midpoint);
                Gizmos.color = Preferences.MissColor.Value;
                Gizmos.DrawLine(midpoint, end);
            }
        }
    }
}