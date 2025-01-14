using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class PreviewForm_Box : PreviewForm<BoxQuery>
    {
        public PreviewForm_Box(BoxQuery query) : base(query)
        {
        }

        public override void DrawCast()
        {
            int hitCount = Query.Cast(out RaycastHit[] hits);
            if (hitCount > 0)
            {
                Vector3 start = Query.GetWorldOrigin();
                Vector3 midpoint = GetBoxCenter(hits[hitCount - 1]);
                Vector3 end = GetEndPoint();
                Handles.color = Color.green;
                Handles.DrawLine(start, midpoint);
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
        public override void DrawOverlap()
        {

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
            Handles.color = color;
            Handles.DrawLine(start, end);
            DrawBox(end, color);
        }
        private void DrawBox(Vector3 center, Color color)
        {
            Quaternion worldOrientation = Query.GetWorldOrientation();
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            center = rotationMatrix.inverse.MultiplyVector(center);
            Handles.matrix = rotationMatrix;
            Handles.color = color;
            Handles.DrawWireCube(center, Query.GetWorldSize());
            Handles.matrix = Matrix4x4.identity;
        }
    }
}