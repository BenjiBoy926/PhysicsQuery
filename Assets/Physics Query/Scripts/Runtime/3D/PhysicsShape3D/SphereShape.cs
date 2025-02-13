using System;
using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, ResultSort3D, PhysicsQuery3D.Shape, AdvancedOptions3D>
    {
        [Serializable]
        public class SphereShape : Shape
        {
            public float Radius => _radius;

            [SerializeField]
            private float _radius = 0.5f;

            public SphereShape()
            {
            }
            public SphereShape(float radius)
            {
                _radius = radius;
            }

            public override bool Cast(Parameters parameters, out RaycastHit hit)
            {
                return Physics.SphereCast(
                    GetRay(parameters),
                    GetWorldRadius(parameters),
                    out hit,
                    parameters.Distance,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
            }
            public override Result<RaycastHit> CastNonAlloc(Parameters parameters)
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
            public override bool Check(Parameters parameters)
            {
                return Physics.CheckSphere(
                    parameters.Origin,
                    GetWorldRadius(parameters),
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
            }
            public override Result<Collider> OverlapNonAlloc(Parameters parameters)
            {
                int count = Physics.OverlapSphereNonAlloc(
                    parameters.Origin,
                    GetWorldRadius(parameters),
                    parameters.ColliderCache,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
                return new(parameters.ColliderCache, count);
            }
            public override void DrawOverlapGizmo(Parameters parameters)
            {
                DrawGizmo(parameters, parameters.Origin);
            }
            public override void DrawGizmo(Parameters parameters, Vector3 center)
            {
                Gizmos.DrawWireSphere(center, GetWorldRadius(parameters));
            }

            public float GetWorldRadius(Parameters parameters)
            {
                Vector3 lossyScale = parameters.LossyScale;
                float max = Mathf.Max(lossyScale.x, lossyScale.y, lossyScale.z);
                return _radius * max;
            }

            private Ray GetRay(Parameters parameters)
            {
                return new(parameters.Origin, parameters.Direction);
            }
        }
    }
}