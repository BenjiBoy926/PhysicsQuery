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

        protected override bool DoPhysicsCast(Ray worldRay, out RaycastHit hit)
        {
            return Physics.SphereCast(worldRay, _radius, out hit, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override int DoPhysicsCastNonAlloc(Ray worldRay, RaycastHit[] cache)
        {
            return Physics.SphereCastNonAlloc(worldRay, _radius, cache, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override bool DoPhysicsCheck(Vector3 worldOrigin)
        {
            return Physics.CheckSphere(worldOrigin, _radius, LayerMask, TriggerInteraction);
        }
        protected override int DoPhysicsOverlapNonAlloc(Vector3 worldOrigin, Collider[] cache)
        {
            return Physics.OverlapSphereNonAlloc(worldOrigin, _radius, cache, LayerMask, TriggerInteraction);
        }
    }
}