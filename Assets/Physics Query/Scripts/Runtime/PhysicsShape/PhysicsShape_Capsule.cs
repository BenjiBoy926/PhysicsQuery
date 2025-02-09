using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Capsule : PhysicsShape
    {
        public readonly struct Position
        {
            public Vector3 Axis => (Cap2 - Cap1) * 0.5f;

            public readonly Vector3 Cap1;
            public readonly Vector3 Cap2;
            public readonly float Radius;

            public Position(PhysicsParameters parameters, Axis axis, float height, float radius)
            {
                float extent = height * 0.5f;
                Vector3 center = parameters.GetWorldStart();
                Vector3 direction = parameters.TransformDirection(axis.Vector);

                Vector3 lossyScale = parameters.LossyScale;
                float distance = extent * lossyScale[axis.Dimension];                
                Vector3 capOffset = direction * distance;
                radius = ScaleRadius(lossyScale, axis, radius);

                Cap1 = center + capOffset;
                Cap2 = center - capOffset;
                Radius = radius;
            }
            private static float ScaleRadius(Vector3 lossyScale, Axis heightAxis, float radius)
            {
                float scale1 = lossyScale[heightAxis.CrossDimension1];
                float scale2 = lossyScale[heightAxis.CrossDimension2];
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

        public override bool Cast(PhysicsParameters parameters, out RaycastHit hit)
        {
            RayDistance worldRay = parameters.GetWorldRay();
            Position position = GetPosition(parameters);
            return Physics.CapsuleCast(
                position.Cap1,
                position.Cap2,
                position.Radius,
                worldRay.Direction,
                out hit,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters parameters)
        {
            RayDistance worldRay = parameters.GetWorldRay();
            Position position = GetPosition(parameters);
            int count = Physics.CapsuleCastNonAlloc(
                position.Cap1,
                position.Cap2,
                position.Radius,
                worldRay.Direction,
                parameters.HitCache,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters parameters)
        {
            Position position = GetPosition(parameters);
            return Physics.CheckCapsule(
                position.Cap1,
                position.Cap2,
                position.Radius,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters parameters)
        {
            Position position = GetPosition(parameters);
            int count = Physics.OverlapCapsuleNonAlloc(
                position.Cap1,
                position.Cap2,
                position.Radius,
                parameters.ColliderCache,
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
            Position position = GetPosition(parameters);
            CapsuleGizmo.Draw(center.Unwrap(), position.Axis, position.Radius);
        }

        public Position GetPosition(PhysicsParameters parameters)
        {
            return new(parameters, _axis, _height, _radius);
        }
    }
}