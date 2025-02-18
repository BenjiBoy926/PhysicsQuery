using UnityEngine;

namespace PQuery
{
    public readonly struct BoxedRaycastHit
    {
        public Component Collider => _info.Collider;
        public int ColliderInstanceID => _info.ColliderInstanceID;
        public float Distance => _info.Distance;
        public Vector3 Normal => _info.Normal;
        public Vector3 Point => _info.Point;
        public Component Rigidbody => _info.Rigidbody;
        public Transform Transform => _info.Transform;

        private readonly AgnosticRaycastHit _info;
        public readonly object Original;

        public BoxedRaycastHit(RaycastHit hit)
        {
            _info = new(hit);
            Original = hit;
        }
        public BoxedRaycastHit(RaycastHit2D hit)
        {
            _info = new(hit);
            Original = hit;
        }
    }
}