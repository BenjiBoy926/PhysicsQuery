using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Capsule : PhysicsShape
    {
        private float Extent => _height / 2;
        public float Radius => _radius;

        [SerializeField]
        private Vector3 _axis = Vector3.up;
        [SerializeField]
        private float _height = 1;
        [SerializeField]
        private float _radius = 0.5f;

        public PhysicsShape_Capsule()
        {
        }
        public PhysicsShape_Capsule(Vector3 axis, float height, float radius)
        {
            _axis = axis;
            _height = height;
            _radius = radius;
        }

        public override bool Cast(PhysicsQuery query, Ray worldRay, out RaycastHit hit)
        {
            var (cap1, cap2) = GetCapPositions(query, worldRay.origin);
            return Physics.CapsuleCast(cap1, cap2, _radius, worldRay.direction, out hit, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        public override int CastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache)
        {
            var (cap1, cap2) = GetCapPositions(query, worldRay.origin);
            return Physics.CapsuleCastNonAlloc(cap1, cap2, _radius, worldRay.direction, cache, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        public override bool Check(PhysicsQuery query, Vector3 worldOrigin)
        {
            var (cap1, cap2) = GetCapPositions(query, worldOrigin);
            return Physics.CheckCapsule(cap1, cap2, _radius, query.LayerMask, query.TriggerInteraction);
        }
        public override int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            var (cap1, cap2) = GetCapPositions(query, worldOrigin);
            return Physics.OverlapCapsuleNonAlloc(cap1, cap2, _radius, cache, query.LayerMask, query.TriggerInteraction);
        }
        public override void DrawOverlapGizmo(PhysicsQuery query)
        {
            DrawGizmo(query, query.GetWorldOrigin());
        }
        public override void DrawGizmo(PhysicsQuery query, Vector3 center)
        {
            CapsuleGizmo.Draw(center, GetWorldAxis(query), _radius);
        }

        public (Vector3, Vector3) GetCapPositions(PhysicsQuery query, Vector3 worldOrigin)
        {
            Vector3 worldAxis = GetWorldAxis(query);
            return (worldOrigin + worldAxis, worldOrigin - worldAxis);
        }
        public Vector3 GetWorldAxis(PhysicsQuery query)
        {
            if (query.Space == Space.World)
            {
                return _axis.normalized * Extent;
            }
            return query.transform.TransformDirection(_axis).normalized * Extent;
        }
    }
}