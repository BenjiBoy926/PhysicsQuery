using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class PreviewForm_Ray : PreviewForm<RayQuery>
    {
        public PreviewForm_Ray(RayQuery query) : base(query)
        {
        }

        public override void DrawCast()
        {
            int hitCount = Query.Cast(out RaycastHit[] hits);
            if (hitCount > 0)
            {
                DrawHits(hits, hitCount);
                DrawNoHit(hits[hitCount - 1].point);
            }
            else
            {
                DrawNoHit();
            }
        }
        public override void DrawOverlap()
        {
            int overlapCount = Query.Overlap(out Collider[] colliders);
            if (overlapCount > 0)
            {
                DrawLineStartToEnd(Color.green);
            }
            else
            {
                DrawNoHit();
            }
        }

        private void DrawHits(RaycastHit[] hits, int count)
        {
            for (int i = 0; i < count; i++)
            {
                DrawHit(hits[i]);
            }
        }
        private void DrawHit(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Handles.color = Color.red;
            Handles.DrawLine(normal.origin, normal.GetPoint(NormalLength));

            Handles.color = Color.green;
            Handles.DrawLine(Query.GetWorldOrigin(), hit.point);
        }
        private void DrawNoHit()
        {
            DrawNoHit(Query.GetWorldOrigin());
        }
        private void DrawNoHit(Vector3 start)
        {
            DrawLineToEnd(start, Color.gray);
        }
        private void DrawLineStartToEnd(Color color)
        {
            DrawLineToEnd(Query.GetWorldOrigin(), color);
        }
        private void DrawLineToEnd(Vector3 start, Color color)
        {
            Vector3 end = Query.GetWorldRay().GetPoint(GetMaxDistance());
            Handles.color = color;
            Handles.DrawLine(start, end);
        }

        private float GetMaxDistance()
        {
            return Mathf.Min(MaxDistance, Query.MaxDistance);
        }
    }
}