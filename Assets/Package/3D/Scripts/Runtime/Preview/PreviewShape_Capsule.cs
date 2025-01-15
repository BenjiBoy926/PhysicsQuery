using NUnit.Framework;
using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Capsule : PreviewShape<CapsuleQuery>
    {
        private const int LineSegmentsPerCircle = 16;

        public PreviewShape_Capsule(CapsuleQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawShape(GetStartPosition(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            Vector3 up = Query.GetWorldAxis();
            Vector3 cap1 = center + up;
            Vector3 cap2 = center - up;
            Vector3 direction = GetWorldRay().direction;
            Vector3 crossAxis = Colinear(up, direction) ? GetArbitraryCrossAxis(up) : direction;
            Vector3 right = Vector3.Cross(up, crossAxis).normalized * Query.Radius;
            Vector3 forward = Vector3.Cross(up, right).normalized * Query.Radius;

            Vector3 left = -right;
            Vector3 down = -up;
            Vector3 back = -forward;

            Gizmos.color = color;
            DrawHemisphere(cap1, right, up, forward);
            DrawHemisphere(cap2, right, down, forward);
            DrawLine(center + forward + up, center + forward + down, color);
            DrawLine(center + back + up, center + back + down, color);
            DrawLine(center + right + up, center + right + down, color);
            DrawLine(center + left + up, center + left + down, color);
        }

        private void DrawHemisphere(Vector3 center, Vector3 right, Vector3 up, Vector3 forward)
        {
            right = right.normalized;
            up = up.normalized;
            forward = forward.normalized;
            DrawCircle(center, forward, right, Query.Radius);
            DrawHalfCircle(center, forward, up, Query.Radius);
            DrawHalfCircle(center, right, up, Query.Radius);
        }
        private void DrawHalfCircle(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius)
        {
            DrawArc(center, xAxis, yAxis, radius, Mathf.PI, LineSegmentsPerCircle / 2);
        }
        private void DrawCircle(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius)
        {
            DrawArc(center, xAxis, yAxis, radius, Mathf.PI * 2, LineSegmentsPerCircle);
        }
        private void DrawArc(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius, float maxAngle, int segmentCount)
        {
            for (int i = 0; i < segmentCount; i++)
            {
                DrawArcSegment(center, xAxis, yAxis, radius, maxAngle, i, segmentCount);
            }
        }
        private void DrawArcSegment(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius, float maxAngle, int segment, int segmentCount)
        {
            Vector3 position1 = GetPositionOnArc(center, xAxis, yAxis, radius, maxAngle, segment, segmentCount);
            Vector3 position2 = GetPositionOnArc(center, xAxis, yAxis, radius, maxAngle, segment + 1, segmentCount);
            Gizmos.DrawLine(position1, position2);
        }
        private Vector3 GetPositionOnArc(Vector3 center, Vector3 xAxis, Vector3 yAxis, float radius, float maxAngle, int position, int segmentCount)
        {
            float proportion = (float)position / segmentCount;
            float angle = maxAngle * proportion;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            return center + xAxis * x + yAxis * y;
        }
        private Vector3 GetArbitraryCrossAxis(Vector3 initial)
        {
            return Colinear(initial, Vector3.up) ? Vector3.right : Vector3.up;
        }
        private bool Colinear(Vector3 a, Vector3 b)
        {
            float angle = Vector3.Angle(a, b);
            return angle < 0.01f || angle > 179.99f;
        }
    }
}