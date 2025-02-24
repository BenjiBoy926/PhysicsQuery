using UnityEngine;

namespace PQuery
{
    public struct EllipseGizmo
    {
        private const int SegmentsPerArc = 32;

        public static void Draw(Vector3 center, Vector3 xAxis, Vector3 yAxis)
        {
            DrawArc(center, xAxis, yAxis, 2 * Mathf.PI);
        }
        public static void DrawHalf(Vector3 center, Vector3 xAxis, Vector3 yAxis)
        {
            DrawArc(center, xAxis, yAxis, Mathf.PI);
        }
        public static void DrawArc(Vector3 center, Vector3 xAxis, Vector3 yAxis, float totalAngle)
        {
            for (int i = 0; i < SegmentsPerArc; i++)
            {
                DrawArcSegment(center, xAxis, yAxis, totalAngle, i);
            }
        }
        private static void DrawArcSegment(Vector3 center, Vector3 xAxis, Vector3 yAxis, float totalAngle, int segment)
        {
            Vector3 position1 = GetPositionOnArc(center, xAxis, yAxis, totalAngle, segment);
            Vector3 position2 = GetPositionOnArc(center, xAxis, yAxis, totalAngle, segment + 1);
            Gizmos.DrawLine(position1, position2);
        }
        private static Vector3 GetPositionOnArc(Vector3 center, Vector3 xAxis, Vector3 yAxis, float totalAngle, int segment)
        {
            float proportion = (float)segment / SegmentsPerArc;
            float angle = totalAngle * proportion;
            float xAngle = Mathf.Cos(angle);
            float yAngle = Mathf.Sin(angle);
            return center + xAxis * xAngle + yAxis * yAngle;
        }
    }
}