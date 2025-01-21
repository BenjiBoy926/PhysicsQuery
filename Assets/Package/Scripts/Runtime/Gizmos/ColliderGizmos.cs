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
            Gizmos.matrix = collider.transform.localToWorldMatrix;
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
            Gizmos.matrix = Matrix4x4.identity;
        }
        private static void DrawBoxColliderGizmo(BoxCollider collider)
        {
            Gizmos.DrawWireCube(collider.center, collider.size);
        }
        private static void DrawSphereColliderGizmo(SphereCollider collider)
        {
            Gizmos.DrawWireSphere(collider.center, collider.radius);        
        }
        private static void DrawCapsuleColliderGizmo(CapsuleCollider collider)
        {
            
        }
        private static void DrawMeshColliderGizmo(MeshCollider collider)
        {
            Gizmos.DrawWireMesh(collider.sharedMesh);
        }
    }
}