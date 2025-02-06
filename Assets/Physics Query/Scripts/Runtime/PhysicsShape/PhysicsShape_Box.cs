using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Box : PhysicsShape
    {
        public Vector3 Size => _size;

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

        public override bool Cast(PhysicsQuery query, RayDistance worldRay, out RaycastHit hit)
        {
            Vector3 extents = GetWorldExtents(query);
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.BoxCast(worldRay.Start, extents, worldRay.Direction, out hit, worldOrientation, worldRay.Distance, query.LayerMask, query.TriggerInteraction);
        }
        public override int CastNonAlloc(PhysicsQuery query, RayDistance worldRay, RaycastHit[] cache)
        {
            Vector3 extents = GetWorldExtents(query);
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.BoxCastNonAlloc(worldRay.Start, extents, worldRay.Direction, cache, worldOrientation, worldRay.Distance, query.LayerMask, query.TriggerInteraction);
        }
        public override bool Check(PhysicsQuery query, Vector3 worldOrigin)
        {
            Vector3 extents = GetWorldExtents(query);
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.CheckBox(worldOrigin, extents, worldOrientation, query.LayerMask, query.TriggerInteraction);
        }
        public override int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            Vector3 extents = GetWorldExtents(query);
            Quaternion worldOrientation = GetWorldOrientation(query);
            return Physics.OverlapBoxNonAlloc(worldOrigin, extents, cache, worldOrientation, query.LayerMask, query.TriggerInteraction);
        }
        public override void DrawOverlapGizmo(PhysicsQuery query)
        {
            DrawGizmo(query, query.GetWorldStart());
        }
        public override void DrawGizmo(PhysicsQuery query, Vector3 center)
        {
            Quaternion worldOrientation = GetWorldOrientation(query);
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            center = rotationMatrix.inverse.MultiplyVector(center);

            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(center, _size);
            Gizmos.matrix = Matrix4x4.identity;
        }

        public Vector3 GetWorldExtents(PhysicsQuery query)
        {
            return GetWorldSize(query) * 0.5f;
        }
        public Vector3 GetWorldSize(PhysicsQuery query)
        {
            if (query.Space == Space.World)
            {
                return _size;
            }
            Vector3 lossyScale = query.transform.lossyScale;
            return new(_size.x * lossyScale.x, _size.y * lossyScale.y, _size.z * lossyScale.z);
        }
        public Quaternion GetWorldOrientation(PhysicsQuery query)
        {
            return query.Space == Space.Self ? query.transform.rotation * _orientation : _orientation;
        }
    }
}