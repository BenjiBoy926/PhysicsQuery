using System;
using UnityEngine;

namespace PhysicsQuery
{
    public class SphereQuery : PhysicsQuery
    {
        public float Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException("Radius must be non-negative");
                }
                _radius = value;
            }
        }

        [Space]
        [SerializeField]
        private float _radius = 0.5f;

        public override PhysicsCastResult Cast()
        {
            Ray worldRay = GetWorldRay();
            RaycastHit[] hits = GetHitCache();
            int count = Physics.SphereCastNonAlloc(worldRay.origin, _radius, worldRay.direction, hits, MaxDistance, LayerMask);
            return new(hits, count);
        }
        public override PhysicsOverlapResult Overlap()
        {
            Vector3 origin = GetWorldOrigin();
            Collider[] overlaps = GetColliderCache();
            int count = Physics.OverlapSphereNonAlloc(origin, _radius, overlaps, LayerMask, TriggerInteraction);
            return new(overlaps, count);
        }
    }
}