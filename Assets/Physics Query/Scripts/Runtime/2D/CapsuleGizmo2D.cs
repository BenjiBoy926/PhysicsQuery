using System;
using UnityEngine;

namespace PQuery
{
    public static class CapsuleGizmo2D
    {
        public static void Draw(Vector2 center, Vector2 size, CapsuleDirection2D direction)
        {
            Vector2 extents = size / 2;
            float length = direction == CapsuleDirection2D.Vertical ? extents.y : extents.x;
            float radius = direction == CapsuleDirection2D.Vertical ? extents.x : extents.y;
            float axisLength = length - radius;
            
            if (axisLength < 1E-6f)
            {
                CircleGizmo2D.Draw(center, radius);
            }
            else
            {
                Vector2 axisDirection = direction == CapsuleDirection2D.Vertical ? Vector2.up : Vector2.right;
                Draw(center, axisDirection * axisLength, radius);
            }
        }
        private static void Draw(Vector2 center, Vector2 axis, float radius)
        {
            Vector2 topCapCenter = center + axis;
            Vector2 bottomCapCenter = center - axis;

            Vector2 down = -axis;
            Vector2 right = Vector2.Perpendicular(axis).normalized * radius;
            Vector2 left = -right;
            Vector2 hemisphereUp = axis.normalized * radius;
            Vector2 hemisphereDown = -hemisphereUp;

            ReadOnlySpan<Vector3> linePoints = stackalloc Vector3[]
            {
                center + axis + right,
                center + down + right,
                center + axis + left,
                center + down + left
            };
            Gizmos.DrawLineList(linePoints);

            new EllipseGizmo(topCapCenter, right, hemisphereUp).DrawHalf();
            new EllipseGizmo(bottomCapCenter, right, hemisphereDown).DrawHalf();
        }
    }
}