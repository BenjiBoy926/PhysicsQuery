using System;
using UnityEngine;

namespace PQuery.Editor
{
    public static class ColliderGizmos2D
    {
        public static void DrawGizmos(Collider2D collider)
        {
            if (collider == null)
            {
                return;
            }
            if (collider is BoxCollider2D boxCollider)
            {
                DrawGizmos(boxCollider);
            }
            else if (collider is CapsuleCollider2D capsuleCollider)
            {
                DrawGizmos(capsuleCollider);
            }
            else if (collider is CircleCollider2D circleCollider)
            {
                DrawGizmos(circleCollider);
            }
        }
        private static void DrawGizmos(BoxCollider2D collider)
        {
            Vector2 extents = collider.size * 0.5f;
            Span<Vector3> corners = stackalloc Vector3[]
            {
                    extents, new(extents.x, -extents.y, 0), -extents, new(-extents.x, extents.y, 0)
            };
            Matrix4x4 matrix = collider.transform.localToWorldMatrix;
            for (int i = 0; i < corners.Length; i++)
            {
                Vector3 world = matrix.MultiplyPoint3x4(corners[i]);
                world.z = 0;
                corners[i] = world;
            }
            Gizmos.DrawLineStrip(corners, true);
        }
        private static void DrawGizmos(CapsuleCollider2D collider)
        {
            CapsuleGizmo2D.Draw(collider.transform.localToWorldMatrix, collider.offset, collider.size, collider.direction);
        }
        private static void DrawGizmos(CircleCollider2D collider)
        {
            Vector3 scale = collider.transform.lossyScale;
            float worldRadius = Mathf.Max(scale.x, scale.y) * collider.radius;
            CircleGizmo2D.Draw(collider.transform.position, worldRadius);
        }
    }
}