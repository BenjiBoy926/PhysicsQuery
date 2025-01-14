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
            Vector3 noHitStart;
            int hitCount = Query.Cast(out RaycastHit[] hits);
            if (hitCount > 0)
            {
                noHitStart = Query.GetWorldRay().GetPoint(hits[hitCount - 1].distance);
                Handles.color = Color.green;
                Handles.DrawLine(Query.GetWorldOrigin(), noHitStart);
                DrawHits(hits, hitCount);
            }
            else
            {
                noHitStart = Query.GetWorldOrigin();
                DrawBox(noHitStart, Color.gray);
            }
            DrawLineAndBox(noHitStart, GetEndPoint(), Color.gray);
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
            Ray worldRay = Query.GetWorldRay();
            Vector3 center = worldRay.GetPoint(hit.distance);
            DrawBox(center, Color.green);
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