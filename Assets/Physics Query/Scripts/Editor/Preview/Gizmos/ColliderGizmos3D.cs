using UnityEngine;

namespace PQuery.Editor
{
    public static class ColliderGizmos3D
    {
        public static void DrawGizmos(Collider collider)
        {
            if (collider == null)
            {
                return;
            }
            if (collider is BoxCollider box)
            {
                DrawBoxColliderGizmo(box);
            }
            else if (collider is SphereCollider sphereCollider)
            {
                DrawSphereColliderGizmo(sphereCollider);
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                DrawCapsuleColliderGizmo(capsuleCollider);
            }
            else if (collider is MeshCollider meshCollider)
            {
                DrawMeshColliderGizmo(meshCollider);
            }
        }
        private static void DrawBoxColliderGizmo(BoxCollider collider)
        {
            Gizmos.matrix = collider.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(collider.center, collider.size);
            Gizmos.matrix = Matrix4x4.identity;
        }
        private static void DrawSphereColliderGizmo(SphereCollider collider)
        {
            Vector3 center = collider.transform.position + collider.center;
            Vector3 scale = collider.transform.lossyScale;
            float maxScale = Mathf.Max(scale.x, scale.y, scale.z);
            float radius = collider.radius * maxScale;
            Gizmos.DrawWireSphere(center, radius);
        }
        private static void DrawCapsuleColliderGizmo(CapsuleCollider collider)
        {
            Vector3 center = collider.transform.TransformPoint(collider.center);
            Vector3 axis = GetWorldAxis(collider);
            float radius = GetWorldRadius(collider);
            CapsuleGizmo3D.Draw(center, axis, radius);
        }
        private static void DrawMeshColliderGizmo(MeshCollider collider)
        {
            Gizmos.matrix = collider.transform.localToWorldMatrix;
            Gizmos.DrawWireMesh(collider.sharedMesh);
            Gizmos.matrix = Matrix4x4.identity;
        }

        private static Vector3 GetWorldAxis(CapsuleCollider collider)
        {
            Vector3 direction = GetWorldDirection(collider);
            float scale = collider.transform.lossyScale[collider.direction];
            float worldExtent = 0.5f * collider.height * scale;
            float worldRadius = GetWorldRadius(collider);
            float axisLength = worldExtent - worldRadius;
            return Mathf.Max(axisLength, 0) * direction;
        }
        private static Vector3 GetWorldDirection(CapsuleCollider collider)
        {
            return collider.direction switch
            {
                0 => collider.transform.right,
                1 => collider.transform.up,
                2 => collider.transform.forward,
                _ => Vector3.zero,
            };
        }
        private static float GetWorldRadius(CapsuleCollider collider)
        {
            int xDirection = (collider.direction + 1) % 3;
            int zDirection = (collider.direction + 2) % 3;

            Vector3 scale = collider.transform.lossyScale;
            float xScale = scale[xDirection];
            float zScale = scale[zDirection];
            return Mathf.Max(xScale, zScale) * collider.radius;
        }
    }
}