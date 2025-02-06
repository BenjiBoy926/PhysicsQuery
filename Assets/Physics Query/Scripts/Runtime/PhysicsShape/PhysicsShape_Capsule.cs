using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Capsule : PhysicsShape
    {
        public readonly struct Parameters
        {
            public Vector3 Axis => (Cap2 - Cap1) * 0.5f;

            public readonly Vector3 Cap1;
            public readonly Vector3 Cap2;
            public readonly float Radius;

            public Parameters(PhysicsQuery query, Vector3 center, Axis axis, float height, float radius)
            {
                float extent = height * 0.5f;
                Vector3 capOffset = axis.Vector * extent;
                if (query.Space == Space.Self)
                {
                    capOffset = query.transform.TransformPoint(capOffset);
                    radius = ScaleRadius(query.transform, axis, radius);
                }
                Cap1 = center + capOffset;
                Cap2 = center - capOffset;
                Radius = radius;
            }
            private static float ScaleRadius(Transform transform, Axis axis, float radius)
            {
                Vector3 lossyScale = transform.lossyScale;
                float scale1 = lossyScale[axis.CrossDimension1];
                float scale2 = lossyScale[axis.CrossDimension2];
                float largetScale = Mathf.Max(scale1, scale2);
                return radius * largetScale;
            }
        }

        [SerializeReference, SubtypeDropdown]
        private Axis _axis = new Axis_Y();
        [SerializeField]
        private float _height = 1;
        [SerializeField]
        private float _radius = 0.5f;

        public PhysicsShape_Capsule()
        {
        }
        public PhysicsShape_Capsule(Axis axis, float height, float radius)
        {
            _axis = axis;
            _height = height;
            _radius = radius;
        }

        public override bool Cast(PhysicsQuery query, RayDistance worldRay, out RaycastHit hit)
        {
            Parameters parameters = GetParameters(query, worldRay.Start);
            return Physics.CapsuleCast(
                parameters.Cap1,
                parameters.Cap2,
                parameters.Radius,
                worldRay.Direction,
                out hit,
                worldRay.Distance,
                query.LayerMask,
                query.TriggerInteraction);
        }
        public override int CastNonAlloc(PhysicsQuery query, RayDistance worldRay, RaycastHit[] cache)
        {
            Parameters parameters = GetParameters(query, worldRay.Start);
            return Physics.CapsuleCastNonAlloc(
                parameters.Cap1,
                parameters.Cap2,
                parameters.Radius,
                worldRay.Direction,
                cache,
                worldRay.Distance,
                query.LayerMask,
                query.TriggerInteraction);
        }
        public override bool Check(PhysicsQuery query, Vector3 worldOrigin)
        {
            Parameters parameters = GetParameters(query, worldOrigin);
            return Physics.CheckCapsule(
                parameters.Cap1,
                parameters.Cap2,
                parameters.Radius,
                query.LayerMask,
                query.TriggerInteraction);
        }
        public override int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            Parameters parameters = GetParameters(query, worldOrigin);
            return Physics.OverlapCapsuleNonAlloc(
                parameters.Cap1,
                parameters.Cap2,
                parameters.Radius,
                cache,
                query.LayerMask,
                query.TriggerInteraction);
        }
        public override void DrawOverlapGizmo(PhysicsQuery query)
        {
            DrawGizmo(query, query.GetWorldStart());
        }
        public override void DrawGizmo(PhysicsQuery query, Vector3 center)
        {
            Parameters parameters = GetParameters(query, center);
            CapsuleGizmo.Draw(center, parameters.Axis, parameters.Radius);
        }

        public Parameters GetParameters(PhysicsQuery query, Vector3 center)
        {
            return new(query, center, _axis, _height, _radius);
        }
    }
}