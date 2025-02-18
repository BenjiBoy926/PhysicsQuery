using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_CastNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            var result = query.AgnosticCastNonAlloc(ResultSortAgnostic.Distance);

            Gizmos.color = Preferences.GetColorForResult(result);
            query.DrawGizmo(query.GetAgnosticWorldStart());

            // Note: we draw each hit and the collider first before the shapes so that the shapes show up on top
            DrawEachHit(result);
            DrawResultLine(query, result);
            DrawShapeAtEachHit(query, result); 

            Gizmos.color = Preferences.MissColor.Value;
            query.DrawGizmo(query.GetAgnosticWorldEnd());
        }
        private void DrawShapeAtEachHit(PhysicsQuery query, Result<AgnosticRaycastHit> result)
        {
            Gizmos.color = result.IsFull ? Preferences.CacheFullColor.Value : Preferences.HitColor.Value;
            for (int i = 0; i < result.Count; i++)
            {
                AgnosticRaycastHit hit = result[i];
                DrawShapeAtHit(query, hit);
            }
        }
        private void DrawEachHit(Result<AgnosticRaycastHit> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                DrawHit(result[i]);
            }
        }
        private void DrawResultLine(PhysicsQuery query, Result<AgnosticRaycastHit> result)
        {
            Vector3 start = query.GetAgnosticWorldStart();
            Vector3 end = query.GetAgnosticWorldEnd();

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
                Vector3 midpoint = GetShapeCenter(query, result.Last);
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start, midpoint);
                Gizmos.color = Preferences.MissColor.Value;
                Gizmos.DrawLine(midpoint, end);
            }
        }
    }
}