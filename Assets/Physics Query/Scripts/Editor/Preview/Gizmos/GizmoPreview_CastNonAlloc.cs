using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_CastNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            var result = query.CastNonAlloc(ResultSort.Distance);
            Color hitShapeColor = result.IsFull ? Preferences.CacheFullColor.Value : Preferences.HitColor.Value;

            Gizmos.color = Preferences.GetColorForResult(result);
            query.DrawGizmo(GetStartPosition(query));
            for (int i = 0; i < result.Count; i++)
            {
                RaycastHit hit = result[i];
                Gizmos.color = hitShapeColor;
                DrawHit(query, hit);
            }
            DrawResultLine(query, result);

            Gizmos.color = Preferences.MissColor.Value;
            query.DrawGizmo(GetEndPosition(query));
        }
        private void DrawResultLine(PhysicsQuery query, Result<RaycastHit> result)
        {
            Vector3 start = GetStartPosition(query);
            Vector3 end = GetEndPosition(query);

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
                Vector3 midpoint = GetWorldRay(query).GetPoint(result.Last.distance);
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start, midpoint);
                Gizmos.color = Preferences.MissColor.Value;
                Gizmos.DrawLine(midpoint, end);
            }
        }
    }
}