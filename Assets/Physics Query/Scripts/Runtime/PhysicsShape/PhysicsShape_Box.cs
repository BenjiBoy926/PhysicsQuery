using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Box : PhysicsShape
    {
        public Vector3 Size => _size;
        private Vector3 Extents => _size / 2;

        [SerializeField]
        private Vector3 _size = Vector3.one;
        [SerializeField]
        private Quaternion _orientation = Quaternion.identity;

        public PhysicsShape_Box()
        {
        }
        public PhysicsShape_Box(Vector3 size, Quaternion orientation)
        {
            _size = size;
            _orientation = orientation;
        }

        public override bool Cast(PhysicsQuery query, Ray worldRay, out RaycastHit hit)
        {
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.BoxCast(worldRay.origin, Extents, worldRay.direction, out hit, worldOrientation, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        public override int CastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache)
        {
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.BoxCastNonAlloc(worldRay.origin, Extents, worldRay.direction, cache, worldOrientation, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        public override bool Check(PhysicsQuery query, Vector3 worldOrigin)
        {
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.CheckBox(worldOrigin, Extents, worldOrientation, query.LayerMask, query.TriggerInteraction);
        }
        public override int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
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