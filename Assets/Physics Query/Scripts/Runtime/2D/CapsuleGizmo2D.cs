using System;
using UnityEngine;

namespace PQuery
{
    public static class CapsuleGizmo2D
    {
        public static void Draw(Matrix4x4 transformation, Vector2 localCenter, Vector2 localSize, CapsuleDirection2D localDirection)
        {
            Vector3 center = transformation.MultiplyPoint3x4(localCenter);
            Vector2 lossyScale = transformation.lossyScale;
            Vector2 size = localSize * lossyScale;
            float radius = GetRadius(size, localDirection);
            float axisLength = GetStraightSideExtent(size, localDirection);

            if (axisLength < 1E-6f)
            {
                CircleGizmo2D.Draw(center, radius);
            }
            else
            {
                Vector2 axisDirection = Vector2.up;
                Draw(center, axisDirection * axisLength, radius);
            }
        }
        private static void Draw(Vector3 center, Vector2 axis, float radius)
        {
            Vector3 topCapCenter = center + (Vector3)axis;
            Vector3 bottomCapCenter = center - (Vector3)axis;

            Vector3 down = -axis;
            Vector3 right = Vector2.Perpendicular(axis).normalized * radius;
            Vector3 left = -right;
            Vector3 hemisphereUp = axis.normalized * radius;
            Vector3 hemisphereDown = -hemisphereUp;

            ReadOnlySpan<Vector3> linePoints = stackalloc Vector3[]
            {
                center + (Vector3)axis + right,
                center + down + right,
                center + (Vector3)axis + left,
                center + down + left
            };
            Gizmos.DrawLineList(linePoints);

            new EllipseGizmo(topCapCenter, right, hemisphereUp).DrawHalf();
            new EllipseGizmo(bottomCapCenter, right, hemisphereDown).DrawHalf();
        }

        private static float GetStraightSideExtent(Vector2 size, CapsuleDirection2D direction)
        {
            return GetStraightSideLength(size, direction) / 2;
        }
        private static float GetStraightSideLength(Vector2 size, CapsuleDirection2D direction)
        {
            return GetOverallLength(size, direction) - GetDiameter(size, direction);
        }
        private static float GetOverallExtent(Vector2 size, CapsuleDirection2D direction)
        {
            return GetOverallLength(size, direction) / 2;
        }
        private static float GetRadius(Vector2 size, CapsuleDirection2D direction)
        {
            return GetDiameter(size, direction) / 2;
        }

        private static float GetOverallLength(Vector2 size, CapsuleDirection2D direction)
        {
            return size.y;
        }
        private static float GetDiameter(Vector2 size, CapsuleDirection2D direction)
        {
            return size.x;
        }
    }
}