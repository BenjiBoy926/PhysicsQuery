using NUnit.Framework;
using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Capsule : PreviewShape<CapsuleQuery>
    {
        public PreviewShape_Capsule(CapsuleQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawShape(GetStartPosition(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            Vector3 offset = Query.GetWorldAxis();
            Vector3 cap1 = center + offset;
            Vector3 cap2 = center - offset;

            Gizmos.color = color;
            Gizmos.DrawWireSphere(cap1, Query.Radius);
            Gizmos.DrawWireSphere(cap2, Query.Radius);

            GetRightAndForward(offset, out Vector3 right, out Vector3 forward);
            DrawCircle(center, offset, Query.Radius);
            DrawHalfCircle(center, right, Query.Radius);
            DrawHalfCircle(center, forward, Query.Radius);
        }

        private void DrawHalfCircle(Vector3 center, Vector3 planeNormal, float radius)
        {
            DrawArc(center, planeNormal, radius, Mathf.PI, 8);
        }
        private void DrawCircle(Vector3 center, Vector3 planeNormal, float radius)
        {
            DrawArc(center, planeNormal, radius, Mathf.PI * 2, 16);
        }
        private void DrawArc(Vector3 center, Vector3 planeNormal, float radius, float maxAngle, int segmentCount)
        {
            for (int i = 0; i < segmentCount; i++)
            {
                DrawArcSegment(center, planeNormal, radius, maxAngle, i, segmentCount);
            }
        }
        private void DrawArcSegment(Vector3 center, Vector3 planeNormal, float radius, float maxAngle, int segment, int segmentCount)
        {
            Vector3 position1 = GetPositionOnArc(center, planeNormal, radius, maxAngle, segment, segmentCount);
            Vector3 position2 = GetPositionOnArc(center, planeNormal, radius, maxAngle, segment + 1, segmentCount);
            Gizmos.DrawLine(position1, position2);
        }
        private Vector3 GetPositionOnArc(Vector3 center, Vector3 planeAxis, float radius, float maxAngle, int position, int segmentCount)
        {
            float proportion = (float)position / segmentCount;
            float angle = maxAngle * proportion;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            GetRightAndForward(planeAxis, out Vector3 right, out Vector3 forward);
            right *= x;
            forward *= y;

            return center + right + forward;
        }
        private void GetRightAndForward(Vector3 up, out Vector3 right, out Vector3 forward)
        {
            Vector3 initialCross = Vector3.Angle(up, Vector3.forward) > 0.001f ? Vector3.forward : Vector3.down;
            right = Vector3.Cross(initialCross, up).normalized;
            forward = Vector3.Cross(up, right).normalized;
        }
    }
}