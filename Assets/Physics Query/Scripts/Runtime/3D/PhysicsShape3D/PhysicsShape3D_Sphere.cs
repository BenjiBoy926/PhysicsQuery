using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape3D_Sphere : PhysicsShape3D
    {
        public float Radius => _radius;

        [SerializeField]
        private float _radius = 0.5f;

        public PhysicsShape3D_Sphere()
        {
        }
        public PhysicsShape3D_Sphere(float radius)
        {
            _radius = radius;
        }

        public override bool Cast(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters, out RaycastHit hit)
        {
            RayDistance<VectorWrapper3D, RayWrapper3D> worldRay = parameters.GetWorldRay();
            return Physics.SphereCast(
                worldRay.Ray.Unwrap(),
                GetWorldRadius(parameters),
                out hit,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            RayDistance<VectorWrapper3D, RayWrapper3D> worldRay = parameters.GetWorldRay();
            int count = Physics.SphereCastNonAlloc(
                worldRay.Ray.Unwrap(),
                GetWorldRadius(parameters),
                parameters.HitCache,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            return Physics.CheckSphere(
                parameters.GetWorldStart().Unwrap(),
                GetWorldRadius(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            int count = Physics.OverlapSphereNonAlloc(
                parameters.GetWorldStart().Unwrap(),
                GetWorldRadius(parameters),
                parameters.ColliderCache,
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
            Gizmos.DrawWireSphere(center.Unwrap(), GetWorldRadius(parameters));
        }

        public float GetWorldRadius(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            Vector3 lossyScale = parameters.LossyScale;
            float max = Mathf.Max(lossyScale.x, lossyScale.y, lossyScale.z);
            return _radius * max;
        }
    }
}