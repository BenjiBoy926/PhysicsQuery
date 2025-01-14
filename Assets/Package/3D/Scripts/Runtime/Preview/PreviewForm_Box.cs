using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewForm_Box : PreviewForm<BoxQuery>
    {
        public PreviewForm_Box(BoxQuery query) : base(query)
        {
        }

        public override void DrawCastGizmos()
        {
            int hitCount = Query.Cast(out RaycastHit[] hits);
            if (hitCount > 0)
            {
                Vector3 start = Query.GetWorldOrigin();
                Vector3 midpoint = GetBoxCenter(hits[hitCount - 1]);
                Vector3 end = GetEndPoint();
                Gizmos.color = Color.green;
                Gizmos.DrawLine(start, midpoint);
                DrawLineAndBox(midpoint, end, Color.gray);
                DrawHits(hits, hitCount);
            }
            else
            {
                Vector3 start = Query.GetWorldOrigin();
                Vector3 end = GetEndPoint();
                DrawBox(start, Color.gray);
                DrawLineAndBox(start, end, Color.gray);
            }
        }
        public override void DrawOverlapGizmos()
        {
            int overlapCount = Query.Overlap(out Collider[] overlaps);
            if (overlapCount > 0)
            {
                DrawBox(Query.GetWorldOrigin(), Color.green);
            }
            else
            {
                DrawBox(Query.GetWorldOrigin(), Color.gray);
            }
        }

        private void DrawHits(RaycastHit[] hits, int hitCount)
        {
            for (int i = 0; i < hitCount; i++)
            {
                DrawHit(hits[i]);
            }
        }
        private void DrawHit(RaycastHit hit)
        {
            DrawBox(GetBoxCenter(hit), Color.green);
        }
        private Vector3 GetBoxCenter(RaycastHit hit)
        {
            Ray worldRay = Query.GetWorldRay();
            return worldRay.GetPoint(hit.distance);
        }
        private void DrawLineAndBox(Vector3 start, Vector3 end, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(start, end);
            DrawBox(end, color);
        }
        private void DrawBox(Vector3 center, Color color)
        {
            Quaternion worldOrientation = Query.GetWorldOrientation();
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            center = rotationMatrix.inverse.MultiplyVector(center);
            Gizmos.matrix = rotationMatrix;
            Gizmos.color = color;
            Gizmos.DrawWireCube(center, Query.GetWorldSize());
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}