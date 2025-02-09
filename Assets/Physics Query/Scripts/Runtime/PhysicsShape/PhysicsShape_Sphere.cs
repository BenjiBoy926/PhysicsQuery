using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Sphere : PhysicsShape
    {
        public float Radius => _radius;

        [SerializeField]
        private float _radius = 0.5f;

        public PhysicsShape_Sphere()
        {
        }
        public PhysicsShape_Sphere(float radius)
        {
            _radius = radius;
        }

        public override bool Cast(PhysicsParameters parameters, out RaycastHit hit)
        {
            RayDistance worldRay = parameters.GetWorldRay();
            return Physics.SphereCast(
                worldRay.Ray,
                GetWorldRadius(parameters),
                out hit,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters parameters)
        {
            RayDistance worldRay = parameters.GetWorldRay();
            int count = Physics.SphereCastNonAlloc(
                worldRay.Ray,
                GetWorldRadius(parameters),
                parameters.HitCache,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters parameters)
        {
            return Physics.CheckSphere(
                parameters.GetWorldStart(),
                GetWorldRadius(parameters),
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters parameters)
        {
            int count = Physics.OverlapSphereNonAlloc(
                parameters.GetWorldStart(),
                GetWorldRadius(parameters),
                parameters.ColliderCache,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.ColliderCache, count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters parameters)
        {
            DrawGizmo(parameters, parameters.GetWorldStart().Wrap());
        }
        public override void DrawGizmo(PhysicsParameters parameters, Vector3Wrapper center)
        {
            Gizmos.DrawWireSphere(center.Unwrap(), GetWorldRadius(parameters));
        }

        public float GetWorldRadius(PhysicsParameters parameters)
        {
            Vector3 lossyScale = parameters.LossyScale;
            float max = Mathf.Max(lossyScale.x, lossyScale.y, lossyScale.z);
            return _radius * max;
        }
    }
}