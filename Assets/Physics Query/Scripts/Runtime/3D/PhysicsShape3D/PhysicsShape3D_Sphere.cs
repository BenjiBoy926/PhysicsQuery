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

        public override bool Cast(PhysicsParameters3D parameters, out RaycastHit hit)
        {
            RayDistance3D worldRay = parameters.GetWorldRay();
            return Physics.SphereCast(
                worldRay.Ray.Unwrap(),
                GetWorldRadius(parameters),
                out hit,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters3D parameters)
        {
            RayDistance3D worldRay = parameters.GetWorldRay();
            int count = Physics.SphereCastNonAlloc(
                worldRay.Ray.Unwrap(),
                GetWorldRadius(parameters),
                parameters.HitCache,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters3D parameters)
        {
            return Physics.CheckSphere(
                parameters.GetWorldStart().Unwrap(),
                GetWorldRadius(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters3D parameters)
        {
            int count = Physics.OverlapSphereNonAlloc(
                parameters.GetWorldStart().Unwrap(),
                GetWorldRadius(parameters),
                parameters.ColliderCache,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.ColliderCache, count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters3D parameters)
        {
            DrawGizmo(parameters, parameters.GetWorldStart());
        }
        public override void DrawGizmo(PhysicsParameters3D parameters, VectorWrapper3D center)
        {
            Gizmos.DrawWireSphere(center.Unwrap(), GetWorldRadius(parameters));
        }

        public float GetWorldRadius(PhysicsParameters3D parameters)
        {
            Vector3 lossyScale = parameters.LossyScale;
            float max = Mathf.Max(lossyScale.x, lossyScale.y, lossyScale.z);
            return _radius * max;
        }
    }
}