using System;
using UnityEngine;

namespace PQuery
{
    public class SphereQuery : PhysicsQuery
    {
        public float Radius
        {
            get => _radius;
            set => _radius = Mathf.Max(value, MinNonZeroFloat);
        }

        [Space]
        [SerializeField]
        private float _radius = 0.5f;

        protected override int DoPhysicsCast(Ray worldRay, RaycastHit[] cache)
        {
            return Physics.SphereCastNonAlloc(worldRay, _radius, cache, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override int DoPhysicsOverlap(Vector3 worldOrigin, Collider[] cache)
        {
            return Physics.OverlapSphereNonAlloc(worldOrigin, _radius, cache, LayerMask, TriggerInteraction);
        }
    }
}