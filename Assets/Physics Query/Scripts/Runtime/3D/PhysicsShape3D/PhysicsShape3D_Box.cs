using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape3D_Box : PhysicsShape3D
    {
        [SerializeField]
        private Vector3 _size = Vector3.one;
        [SerializeField]
        private Quaternion _orientation = Quaternion.identity;

        public PhysicsShape3D_Box()
        {
        }
        public PhysicsShape3D_Box(Vector3 size, Quaternion orientation)
        {
            _size = size;
            _orientation = orientation;
        }

        public override bool Cast(PhysicsParameters<Vector3, RaycastHit, Collider> parameters, out RaycastHit hit)
        {
            return Physics.BoxCast(
                parameters.Origin,
                GetWorldExtents(parameters),
                parameters.Direction,
                out hit,
                GetWorldOrientation(parameters),
                parameters.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            int count = Physics.BoxCastNonAlloc(
                parameters.Origin,
                GetWorldExtents(parameters),
                parameters.Direction,
                parameters.HitCache,
                GetWorldOrientation(parameters),
                parameters.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            return Physics.CheckBox(
                parameters.Origin,
                GetWorldExtents(parameters),
                GetWorldOrientation(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            int count = Physics.OverlapBoxNonAlloc(
                parameters.Origin,
                GetWorldExtents(parameters),
                parameters.ColliderCache,
                GetWorldOrientation(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.ColliderCache, count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            DrawGizmo(parameters, parameters.Origin);
        }
        public override void DrawGizmo(PhysicsParameters<Vector3, RaycastHit, Collider> parameters, Vector3 center)
        {
            Gizmos.matrix = Matrix4x4.TRS(center, GetWorldOrientation(parameters), parameters.LossyScale);
            Gizmos.DrawWireCube(Vector3.zero, _size);
            Gizmos.matrix = Matrix4x4.identity;
        }

        public Vector3 GetWorldExtents(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            return GetWorldSize(parameters) * 0.5f;
        }
        public Vector3 GetWorldSize(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            return parameters.TransformScale(_size);
        }
        public Quaternion GetWorldOrientation(PhysicsParameters<Vector3, RaycastHit, Collider> parameters)
        {
            return parameters.TransformRotation(_orientation);
        }
    }
}