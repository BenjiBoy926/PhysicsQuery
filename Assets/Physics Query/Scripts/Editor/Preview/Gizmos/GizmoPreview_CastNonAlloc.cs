using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoPreview_CastNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery3D query)
        {
            var result = query.CastNonAlloc(ResultSort3D.Distance);

            Gizmos.color = Preferences.GetColorForResult(result);
            query.DrawGizmo(query.GetWorldStart());

            // Note: we draw each hit and the collider first before the shapes so that the shapes show up on top
            DrawEachHit(result);
            DrawResultLine(query, result);
            DrawShapeAtEachHit(query, result); 

            Gizmos.color = Preferences.MissColor.Value;
            query.DrawGizmo(query.GetWorldEnd());
        }
        private void DrawShapeAtEachHit(PhysicsQuery3D query, Result<RaycastHit> result)
        {
            Gizmos.color = result.IsFull ? Preferences.CacheFullColor.Value : Preferences.HitColor.Value;
            for (int i = 0; i < result.Count; i++)
            {
                RaycastHit hit = result[i];
                DrawShapeAtHit(query, hit);
            }
        }
        private void DrawEachHit(Result<RaycastHit> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                DrawHit(result[i]);
            }
        }
        private void DrawResultLine(PhysicsQuery3D query, Result<RaycastHit> result)
        {
            Vector3 start = query.GetWorldStart().Unwrap();
            Vector3 end = query.GetWorldEnd().Unwrap();

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
                Vector3 midpoint = query.GetWorldRay().GetPoint(result.Last.distance).Unwrap();
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start, midpoint);
                Gizmos.color = Preferences.MissColor.Value;
                Gizmos.DrawLine(midpoint, end);
            }
        }
    }
}