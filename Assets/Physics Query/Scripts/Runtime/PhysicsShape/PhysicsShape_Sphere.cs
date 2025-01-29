using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Sphere : PhysicsShape
    {
        public float Radius => _radius;

        [SerializeField]
        private float _radius = 0.5f;

        public PhysicsShape_Sphere()
        {
        }
        public PhysicsShape_Sphere(float radius)
        {
            _radius = radius;
        }

        protected override bool DoPhysicsCast(PhysicsQuery query, Ray worldRay, out RaycastHit hit)
        {
            return Physics.SphereCast(worldRay, _radius, out hit, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        protected override int DoPhysicsCastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache)
        {
            return Physics.SphereCastNonAlloc(worldRay, _radius, cache, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        protected override bool DoPhysicsCheck(PhysicsQuery query, Vector3 worldOrigin)
        {
            return Physics.CheckSphere(worldOrigin, _radius, query.LayerMask, query.TriggerInteraction);
        }
        protected override int DoPhysicsOverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            return Physics.OverlapSphereNonAlloc(worldOrigin, _radius, cache, query.LayerMask, query.TriggerInteraction);
        }
    }
}