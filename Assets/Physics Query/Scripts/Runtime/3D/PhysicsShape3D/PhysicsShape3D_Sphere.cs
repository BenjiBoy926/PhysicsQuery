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

        public override bool Cast(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters, out RaycastHit hit)
        {
            return Physics.SphereCast(
                GetRay(parameters),
                GetWorldRadius(parameters),
                out hit,
                parameters.Distance,
                parameters.Advanced.LayerMask,
                parameters.Advanced.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            int count = Physics.SphereCastNonAlloc(
                GetRay(parameters),
                GetWorldRadius(parameters),
                parameters.HitCache,
                parameters.Distance,
                parameters.Advanced.LayerMask,
                parameters.Advanced.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            return Physics.CheckSphere(
                parameters.Origin,
                GetWorldRadius(parameters),
                parameters.Advanced.LayerMask,
                parameters.Advanced.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            int count = Physics.OverlapSphereNonAlloc(
                parameters.Origin,
                GetWorldRadius(parameters),
                parameters.ColliderCache,
                parameters.Advanced.LayerMask,
                parameters.Advanced.TriggerInteraction);
            return new(parameters.ColliderCache, count);
        }
        public override void DrawOverlapGizmo(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            DrawGizmo(parameters, parameters.Origin);
        }
        public override void DrawGizmo(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters, Vector3 center)
        {
            Gizmos.DrawWireSphere(center, GetWorldRadius(parameters));
        }

        public float GetWorldRadius(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            Vector3 lossyScale = parameters.LossyScale;
            float max = Mathf.Max(lossyScale.x, lossyScale.y, lossyScale.z);
            return _radius * max;
        }

        private Ray GetRay(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            return new(parameters.Origin, parameters.Direction);
        }
    }
}