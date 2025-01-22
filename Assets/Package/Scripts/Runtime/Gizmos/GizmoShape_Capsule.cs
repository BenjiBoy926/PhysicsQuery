using NUnit.Framework;
using UnityEngine;

namespace PhysicsQuery
{
    public class GizmoShape_Capsule : GizmoShape<CapsuleQuery>
    {
        private const int LineSegmentsPerCircle = 16;

        public GizmoShape_Capsule(CapsuleQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawShape(GetStartPosition(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            Vector3 up = Query.GetWorldAxis();
            float radius = Query.Radius;
            Gizmos.color = color;
            DrawCapsule(center, up, radius);
        }

        private static void DrawCapsule(Vector3 center, Vector3 up, float radius)
        {
            Vector3 topCapCenter = center + up;
            Vector3 bottomCapCenter = center - up;
            Vector3 crossAxis = GetArbitraryCrossAxis(up);
            Vector3 right = Vector3.Cross(up, crossAxis).normalized * radius;
            Vector3 forward = Vector3.Cross(up, right).normalized * radius;

            Vector3 left = -right;
            Vector3 down = -up;
            Vector3 back = -forward;

            DrawHemisphere(topCapCenter, right, up, forward, radius);
            DrawHemisphere(bottomCapCenter, right, down, forward, radius);
            Gizmos.DrawLine(center + forward + up, center + forward + down);
            Gizmos.DrawLine(center + back + up, center + back + down);
            Gizmos.DrawLine(center + right + up, center + right + down);
            Gizmos.DrawLine(center + left + up, center + left + down);
        }
        private static void DrawHemisphere(Vector3 center, Vector3 right, Vector3 up, Vector3 forward, float radius)
        {
            right = right.normalized;
            up = up.normalized;
            forward = forward.normalized;
            DrawCircle(center, forward, right, radius);
            DrawHalfCircle(center, forward, up, radius);
            DrawHalfCircle(center, right, up, radius);
        }
        private static void DrawHalfCircle(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius)
        {
            DrawArc(center, xAxis, yAxis, radius, Mathf.PI, LineSegmentsPerCircle / 2);
        }
        private static void DrawCircle(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius)
        {
            DrawArc(center, xAxis, yAxis, radius, Mathf.PI * 2, LineSegmentsPerCircle);
        }
        private static void DrawArc(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius, float maxAngle, int segmentCount)
        {
            for (int i = 0; i < segmentCount; i++)
            {
                DrawArcSegment(center, xAxis, yAxis, radius, maxAngle, i, segmentCount);
            }
        }
        private static void DrawArcSegment(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius, float maxAngle, int segment, int segmentCount)
        {
            Vector3 position1 = GetPositionOnArc(center, xAxis, yAxis, radius, maxAngle, segment, segmentCount);
            Vector3 position2 = GetPositionOnArc(center, xAxis, yAxis, radius, maxAngle, segment + 1, segmentCount);
            Gizmos.DrawLine(position1, position2);
        }
        private static Vector3 GetPositionOnArc(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius, float maxAngle, int position, int segmentCount)
        {
            float proportion = (float)position / segmentCount;
            float angle = maxAngle * proportion;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            return center + xAxis * x + yAxis * y;
        }
        private static Vector3 GetArbitraryCrossAxis(Vector3 initial)
        {
            return Colinear(initial, Vector3.up) ? Vector3.right : Vector3.up;
        }
        private static bool Colinear(Vector3 a, Vector3 b)
        {
            float angle = Vector3.Angle(a, b);
            return angle < 0.01f || angle > 179.99f;
        }
    }
}