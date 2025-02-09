using UnityEngine;

namespace PQuery
{
    public readonly struct EllipseGizmo3D
    {
        private const int SegmentsPerArc = 32;

        private readonly Vector3 _center;
        private readonly Vector3 _xAxis;
        private readonly Vector3 _yAxis;

        public EllipseGizmo3D(Vector3 center, Vector3 xAxis, Vector3 yAxis)
        {
            _center = center;
            _xAxis = xAxis;
            _yAxis = yAxis;
        }

        public void Draw()
        {
            DrawArc(2 * Mathf.PI);
        }
        public void DrawHalf()
        {
            DrawArc(Mathf.PI);
        }
        public void DrawArc(float totalAngle)
        {
            for (int i = 0; i < SegmentsPerArc; i++)
            {
                DrawArcSegment(totalAngle, i);
            }
        }
        private void DrawArcSegment(float totalAngle, int segment)
        {
            Vector3 position1 = GetPositionOnArc(totalAngle, segment);
            Vector3 position2 = GetPositionOnArc(totalAngle, segment + 1);
            Gizmos.DrawLine(position1, position2);
        }
        private Vector3 GetPositionOnArc(float totalAngle, int segment)
        {
            float proportion = (float)segment / SegmentsPerArc;
            float angle = totalAngle * proportion;
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            return _center + _xAxis * x + _yAxis * y;
        }
    }
}