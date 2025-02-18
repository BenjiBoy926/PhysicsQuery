using UnityEngine;

namespace PQuery
{
    public readonly struct MinimalRaycastHit
    {
        public readonly Component Collider;
        public readonly int ColliderInstanceID;
        public readonly float Distance;
        public readonly Vector3 Normal;
        public readonly Vector3 Point;
        public readonly Component Rigidbody;
        public readonly Transform Transform;

        public MinimalRaycastHit(RaycastHit hit)
        {
            Collider = hit.collider;
            ColliderInstanceID = hit.colliderInstanceID;
            Distance = hit.distance;
            Normal = hit.normal;
            Point = hit.point;
            Rigidbody = hit.rigidbody;
            Transform = hit.transform;
        }
        public MinimalRaycastHit(RaycastHit2D hit)
        {
            Collider = hit.collider;
            ColliderInstanceID = hit.collider.GetInstanceID();
            Distance = hit.distance;
            Normal = hit.normal;
            Point = hit.point;
            Rigidbody = hit.rigidbody;
            Transform = hit.transform;
        }
    }
}