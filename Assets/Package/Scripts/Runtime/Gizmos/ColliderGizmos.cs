using UnityEngine;

namespace PhysicsQuery
{
    public static class ColliderGizmos
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
            
        }
        private static void DrawMeshColliderGizmo(MeshCollider collider)
        {
            Gizmos.matrix = collider.transform.localToWorldMatrix;
            Gizmos.DrawWireMesh(collider.sharedMesh);
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}