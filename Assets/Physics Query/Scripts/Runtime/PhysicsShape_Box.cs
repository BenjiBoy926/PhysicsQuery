using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Box : PhysicsShape
    {
        public Vector3 Size
        {
            get => _size;
            set => _size = value;
        }
        public Vector3 Extents
        {
            get => _size / 2;
            set => _size = value * 2;
        }
        public Quaternion Orientation
        {
            get => _orientation;
            set => _orientation = value;
        }

        [SerializeField]
        private Vector3 _size = Vector3.one;
        [SerializeField]
        private Quaternion _orientation = Quaternion.identity;

        protected override bool DoPhysicsCast(PhysicsQuery query, Ray worldRay, out RaycastHit hit)
        {
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.BoxCast(worldRay.origin, Extents, worldRay.direction, out hit, worldOrientation, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        protected override int DoPhysicsCastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache)
        {
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.BoxCastNonAlloc(worldRay.origin, Extents, worldRay.direction, cache, worldOrientation, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        protected override bool DoPhysicsCheck(PhysicsQuery query, Vector3 worldOrigin)
        {
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.CheckBox(worldOrigin, Extents, worldOrientation, query.LayerMask, query.TriggerInteraction);
        }
        protected override int DoPhysicsOverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.OverlapBoxNonAlloc(worldOrigin, Extents, cache, worldOrientation, query.LayerMask, query.TriggerInteraction);
        }

        public Quaternion GetWorldOrientation(PhysicsQuery query)
        {
            return query.Space == Space.Self ? query.transform.rotation * _orientation : _orientation;
        }

    }
}