using System;
using UnityEngine;

namespace PQuery
{
    public struct SquareGizmo2D
    {
        public static void Draw(Matrix4x4 transformation, Vector2 localPosition, Vector2 localSize)
        {
            float z = transformation.GetPosition().z;
            Vector3 localPosition3 = localPosition;
            Vector3 extents = localSize * 0.5f;
            Span<Vector3> corners = stackalloc Vector3[]
            {
                localPosition3 + extents,
                localPosition3 + new Vector3(extents.x, -extents.y), 
                localPosition3 - extents, 
                localPosition3 + new Vector3(-extents.x, extents.y)
            };
            for (int i = 0; i < corners.Length; i++)
            {
                Vector3 world = transformation.MultiplyPoint3x4(corners[i]);
                world.z = z;
                corners[i] = world;
            }
            Gizmos.DrawLineStrip(corners, true);
        }
    }
}