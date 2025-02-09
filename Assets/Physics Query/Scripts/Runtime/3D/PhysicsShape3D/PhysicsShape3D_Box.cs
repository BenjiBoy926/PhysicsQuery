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

        public override bool Cast(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters, out RaycastHit hit)
        {
            RayDistance<VectorWrapper3D, RayWrapper3D> worldRay = parameters.GetWorldRay();
            return Physics.BoxCast(
                worldRay.Start.Unwrap(),
                GetWorldExtents(parameters),
                worldRay.Direction.Unwrap(),
                out hit,
                GetWorldOrientation(parameters),
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            RayDistance<VectorWrapper3D, RayWrapper3D> worldRay = parameters.GetWorldRay();
            int count = Physics.BoxCastNonAlloc(
                worldRay.Start.Unwrap(),
                GetWorldExtents(parameters),
                worldRay.Direction.Unwrap(),
                parameters.HitCache,
                GetWorldOrientation(parameters),
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            return Physics.CheckBox(
                parameters.GetWorldStart().Unwrap(),
                GetWorldExtents(parameters),
                GetWorldOrientation(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            int count = Physics.OverlapBoxNonAlloc(
                parameters.GetWorldStart().Unwrap(),
                GetWorldExtents(parameters),
                parameters.ColliderCache,
                GetWorldOrientation(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.ColliderCache, count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            DrawGizmo(parameters, parameters.GetWorldStart());
        }
        public override void DrawGizmo(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters, VectorWrapper3D center)
        {
            Quaternion worldOrientation = GetWorldOrientation(parameters);
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            Vector3 invertedCenter = rotationMatrix.inverse.MultiplyVector(center.Unwrap());

            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(invertedCenter, GetWorldSize(parameters));
            Gizmos.matrix = Matrix4x4.identity;
        }

        public Vector3 GetWorldExtents(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            return GetWorldSize(parameters) * 0.5f;
        }
        public Vector3 GetWorldSize(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            return parameters.TransformScale(_size.Wrap()).Unwrap();
        }
        public Quaternion GetWorldOrientation(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            return parameters.TransformRotation(_orientation);
        }
    }
}