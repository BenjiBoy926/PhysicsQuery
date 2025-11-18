using UnityEngine;
using System;

namespace PQuery
{
    public struct PolygonGizmo2D
    {
        public static void Draw(Matrix4x4 transformation, Vector2[] points)
        {
            Span<Vector3> transformedPoints = stackalloc Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 newPoint = transformation.MultiplyPoint3x4(points[i]);
                newPoint.z = 0;
                transformedPoints[i] = newPoint;
            }
            Gizmos.DrawLineStrip(transformedPoints, true);
        }
    }
}