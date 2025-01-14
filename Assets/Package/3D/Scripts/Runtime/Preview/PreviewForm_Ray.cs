using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewForm_Ray : PreviewForm<RayQuery>
    {
        public PreviewForm_Ray(RayQuery query) : base(query)
        {
        }

        public override void DrawCastGizmos()
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
        public override void DrawOverlapGizmos()
        {
            int overlapCount = Query.Overlap(out Collider[] colliders);
            if (overlapCount > 0)
            {
                // TODO: draw an outline of the colliders
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
                Vector3 start = i == 0 ? Query.GetWorldOrigin() : hits[i - 1].point;
                DrawHit(start, hits[i]);
            }
        }
        private void DrawHit(Vector3 start, RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(normal.origin, normal.GetPoint(NormalLength));

            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, hit.point);
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
            Gizmos.color = color;
            Gizmos.DrawLine(start, GetEndPoint());
        }
    }
}