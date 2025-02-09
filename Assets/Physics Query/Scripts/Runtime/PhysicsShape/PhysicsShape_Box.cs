using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Box : PhysicsShape
    {
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

        public override bool Cast(PhysicsParameters parameters, out RaycastHit hit)
        {
            RayDistance worldRay = parameters.GetWorldRay();
            return Physics.BoxCast(
                worldRay.Start,
                GetWorldExtents(parameters),
                worldRay.Direction,
                out hit,
                GetWorldOrientation(parameters),
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters parameters)
        {
            RayDistance worldRay = parameters.GetWorldRay();
            int count = Physics.BoxCastNonAlloc(
                worldRay.Start,
                GetWorldExtents(parameters),
                worldRay.Direction,
                parameters.HitCache,
                GetWorldOrientation(parameters),
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters parameters)
        {
            return Physics.CheckBox(
                parameters.GetWorldStart(),
                GetWorldExtents(parameters),
                GetWorldOrientation(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters parameters)
        {
            int count = Physics.OverlapBoxNonAlloc(
                parameters.GetWorldStart(),
                GetWorldExtents(parameters),
                parameters.ColliderCache,
                GetWorldOrientation(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.ColliderCache, count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters parameters)
        {
            DrawGizmo(parameters, new(parameters.GetWorldStart()));
        }
        public override void DrawGizmo(PhysicsParameters parameters, Vector3Wrapper center)
        {
            Quaternion worldOrientation = GetWorldOrientation(parameters);
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            Vector3 invertedCenter = rotationMatrix.inverse.MultiplyVector(center.Unwrap());

            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(invertedCenter, GetWorldSize(parameters));
            Gizmos.matrix = Matrix4x4.identity;
        }

        public Vector3 GetWorldExtents(PhysicsParameters parameters)
        {
            return GetWorldSize(parameters) * 0.5f;
        }
        public Vector3 GetWorldSize(PhysicsParameters parameters)
        {
            return parameters.TransformScale(_size);
        }
        public Quaternion GetWorldOrientation(PhysicsParameters parameters)
        {
            return parameters.TransformRotation(_orientation);
        }
    }
}