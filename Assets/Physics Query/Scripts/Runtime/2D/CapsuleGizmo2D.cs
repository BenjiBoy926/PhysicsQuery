using System;
using UnityEngine;

namespace PQuery
{
    public static class CapsuleGizmo2D
    {
        public static void Draw(Matrix4x4 transformation, Vector2 localCenter, Vector2 localSize, CapsuleDirection2D direction)
        {
            float radius = GetRadius(transformation, localSize, direction);
            float axisLength = GetStraightSideExtent(transformation, localSize, direction);

            if (axisLength < 1E-6f)
            {
                CircleGizmo2D.Draw(transformation.MultiplyPoint3x4(localCenter), radius);
            }
            else
            {
                Draw(transformation, localCenter, radius, axisLength, direction);
            }
        }

        private static void Draw(Matrix4x4 transformation, Vector2 localCenter, float radius, float axisLength, CapsuleDirection2D direction)
        {
            Vector3 center = transformation.MultiplyPoint3x4(localCenter);
            Vector2 axisDirection = GetLocalLengthAxis(transformation, direction);
            axisDirection = Vector3.ProjectOnPlane(axisDirection, Vector3.forward);
            axisDirection = axisDirection.normalized;
            Draw(center, axisDirection * axisLength, radius);
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

        private static float GetStraightSideExtent(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            return GetStraightSideLength(transformation, size, direction) / 2;
        }
        private static float GetStraightSideLength(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            return GetOverallLength(transformation, size, direction) - GetDiameter(transformation, size, direction);
        }
        private static float GetOverallExtent(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            return GetOverallLength(transformation, size, direction) / 2;
        }
        private static float GetRadius(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            return GetDiameter(transformation, size, direction) / 2;
        }

        private static float GetOverallLength(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            size = ScaleSize(transformation, size);
            return direction == CapsuleDirection2D.Vertical ? size.y : size.x;
        }
        private static float GetDiameter(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            size = ScaleSize(transformation, size);
            return direction == CapsuleDirection2D.Vertical ? size.x : size.y;
        }

        private static Vector2 ScaleSize(Matrix4x4 transformation, Vector2 size)
        {
            Vector3 right = transformation.GetColumn(0);
            Vector3 up = transformation.GetColumn(1);
            Vector3 projectedRight = Vector3.ProjectOnPlane(right, Vector3.forward);
            Vector3 projectedUp = Vector3.ProjectOnPlane(up, Vector3.forward);
            return new(size.x * projectedRight.magnitude, size.y * projectedUp.magnitude);
        }

        private static Vector3 GetLocalLengthAxis(Matrix4x4 transformation, CapsuleDirection2D direction)
        {
            return direction == CapsuleDirection2D.Vertical ? transformation.GetColumn(1) : transformation.GetColumn(0);
        }
        private static Vector3 GetLocalWidthAxis(Matrix4x4 transformation, CapsuleDirection2D direction)
        {
            return direction == CapsuleDirection2D.Vertical ? transformation.GetColumn(0) : transformation.GetColumn(1);
        }
    }
}