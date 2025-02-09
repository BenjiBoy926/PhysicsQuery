using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape3D_Capsule : PhysicsShape3D
    {
        public readonly struct Position
        {
            public Vector3 Axis => (Cap2 - Cap1) * 0.5f;

            public readonly Vector3 Cap1;
            public readonly Vector3 Cap2;
            public readonly float Radius;

            public Position(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters, Axis3D axis, float height, float radius)
            {
                float extent = height * 0.5f;
                Vector3 center = parameters.GetWorldStart().Unwrap();
                Vector3 direction = parameters.TransformDirection(axis.Vector.Wrap()).Unwrap();

                Vector3 lossyScale = parameters.LossyScale;
                float distance = extent * lossyScale[axis.Dimension];                
                Vector3 capOffset = direction * distance;
                radius = ScaleRadius(lossyScale, axis, radius);

                Cap1 = center + capOffset;
                Cap2 = center - capOffset;
                Radius = radius;
            }
            private static float ScaleRadius(Vector3 lossyScale, Axis3D heightAxis, float radius)
            {
                float scale1 = lossyScale[heightAxis.CrossDimension1];
                float scale2 = lossyScale[heightAxis.CrossDimension2];
                float largetScale = Mathf.Max(scale1, scale2);
                return radius * largetScale;
            }
        }

        [SerializeReference, SubtypeDropdown]
        private Axis3D _axis = new Axis_Y();
        [SerializeField]
        private float _height = 1;
        [SerializeField]
        private float _radius = 0.5f;

        public PhysicsShape3D_Capsule()
        {
        }
        public PhysicsShape3D_Capsule(Axis3D axis, float height, float radius)
        {
            _axis = axis;
            _height = height;
            _radius = radius;
        }

        public override bool Cast(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters, out RaycastHit hit)
        {
            RayDistance<VectorWrapper3D, RayWrapper3D> worldRay = parameters.GetWorldRay();
            Position position = GetPosition(parameters);
            return Physics.CapsuleCast(
                position.Cap1,
                position.Cap2,
                position.Radius,
                worldRay.Direction.Unwrap(),
                out hit,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            RayDistance<VectorWrapper3D, RayWrapper3D> worldRay = parameters.GetWorldRay();
            Position position = GetPosition(parameters);
            int count = Physics.CapsuleCastNonAlloc(
                position.Cap1,
                position.Cap2,
                position.Radius,
                worldRay.Direction.Unwrap(),
                parameters.HitCache,
                worldRay.Distance,
                parameters.LayerMask,
                parameters.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            Position position = GetPosition(parameters);
            return Physics.CheckCapsule(
                position.Cap1,
                position.Cap2,
                position.Radius,
                parameters.LayerMask,
                parameters.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
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
        public override void DrawOverlapGizmo(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            DrawGizmo(parameters, parameters.GetWorldStart());
        }
        public override void DrawGizmo(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters, VectorWrapper3D center)
        {
            Position position = GetPosition(parameters);
            CapsuleGizmo3D.Draw(center.Unwrap(), position.Axis, position.Radius);
        }

        public Position GetPosition(PhysicsParameters<VectorWrapper3D, RayWrapper3D, RaycastHit, Collider> parameters)
        {
            return new(parameters, _axis, _height, _radius);
        }
    }
}