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

            public Position(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters, Axis3D axis, float height, float radius)
            {
                float extent = height * 0.5f;
                Vector3 center = parameters.Origin;
                Vector3 direction = parameters.TransformDirection(axis.Vector);

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

        public override bool Cast(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters, out RaycastHit hit)
        {
            Position position = GetPosition(parameters);
            return Physics.CapsuleCast(
                position.Cap1,
                position.Cap2,
                position.Radius,
                parameters.Direction,
                out hit,
                parameters.Distance,
                parameters.Advanced.LayerMask,
                parameters.Advanced.TriggerInteraction);
        }
        public override Result<RaycastHit> CastNonAlloc(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            Position position = GetPosition(parameters);
            int count = Physics.CapsuleCastNonAlloc(
                position.Cap1,
                position.Cap2,
                position.Radius,
                parameters.Direction,
                parameters.HitCache,
                parameters.Distance,
                parameters.Advanced.LayerMask,
                parameters.Advanced.TriggerInteraction);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            Position position = GetPosition(parameters);
            return Physics.CheckCapsule(
                position.Cap1,
                position.Cap2,
                position.Radius,
                parameters.Advanced.LayerMask,
                parameters.Advanced.TriggerInteraction);
        }
        public override Result<Collider> OverlapNonAlloc(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            Position position = GetPosition(parameters);
            int count = Physics.OverlapCapsuleNonAlloc(
                position.Cap1,
                position.Cap2,
                position.Radius,
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
            Position position = GetPosition(parameters);
            CapsuleGizmo3D.Draw(center, position.Axis, position.Radius);
        }

        public Position GetPosition(PhysicsParameters<Vector3, RaycastHit, Collider, AdvancedOptions3D> parameters)
        {
            return new(parameters, _axis, _height, _radius);
        }
    }
}