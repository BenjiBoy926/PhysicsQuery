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

            DrawCircle(center, offset, Query.Radius);
        }

        private void DrawHalfCircle(Vector3 center, Vector3 planeAxis, float radius)
        {
            int segmentCount = 8;
            for (int i = 0; i < segmentCount; i++)
            {

            }
        }
        private void DrawCircle(Vector3 center, Vector3 planeNormal, float radius)
        {
            float maxAngle = Mathf.PI * 2;
            int segmentCount = 16;

            for (int i = 0; i < segmentCount; i++)
            {
                Vector3 position1 = GetPositionOnArc(center, planeNormal, radius, maxAngle, i, segmentCount);
                Vector3 position2 = GetPositionOnArc(center, planeNormal, radius, maxAngle, i + 1, segmentCount);

                Gizmos.DrawLine(position1, position2);
            }
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