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
            Matrix4x4 transformation = collider.transform.localToWorldMatrix;
            SquareGizmo2D.Draw(transformation, collider.offset, collider.size);
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