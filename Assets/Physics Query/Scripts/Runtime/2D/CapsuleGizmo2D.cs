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
            float radius = GetRadius(transformation, size, localDirection);
            float axisLength = GetStraightSideExtent(transformation, size, localDirection);

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

        private static float GetStraightSideExtent(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            return GetStraightSideLength(transformation, size, direction) / 2;
        }
        private static float GetStraightSideLength(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            return GetOverallLength(size, direction) - GetDiameter(transformation, size, direction);
        }
        private static float GetOverallExtent(Vector2 size, CapsuleDirection2D direction)
        {
            return GetOverallLength(size, direction) / 2;
        }
        private static float GetRadius(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            return GetDiameter(transformation, size, direction) / 2;
        }

        private static float GetOverallLength(Vector2 size, CapsuleDirection2D direction)
        {
            return direction == CapsuleDirection2D.Vertical ? size.y : size.x;
        }
        private static float GetDiameter(Matrix4x4 transformation, Vector2 size, CapsuleDirection2D direction)
        {
            Vector3 local = GetLocalWidthAxis(transformation, direction);
            Vector3 world = GetWorldWidthAxis(direction);
            float sizeScale = Vector3.Dot(local, world);
            size *= Mathf.Abs(sizeScale);
            return direction == CapsuleDirection2D.Vertical ? size.x : size.y;
        }

        private static Vector3 GetLocalLengthAxis(Matrix4x4 transformation, CapsuleDirection2D direction)
        {
            return direction == CapsuleDirection2D.Vertical ? transformation.GetColumn(1) : transformation.GetColumn(0);
        }
        private static Vector3 GetLocalWidthAxis(Matrix4x4 transformation, CapsuleDirection2D direction)
        {
            return direction == CapsuleDirection2D.Vertical ? transformation.GetColumn(0) : transformation.GetColumn(1);
        }

        private static Vector3 GetWorldLengthAxis(CapsuleDirection2D direction)
        {
            return direction == CapsuleDirection2D.Vertical ? Vector3.up : Vector3.right;
        }
        private static Vector3 GetWorldWidthAxis(CapsuleDirection2D direction) 
        {
            return direction == CapsuleDirection2D.Vertical ? Vector3.right : Vector3.up;
        }
    }
}