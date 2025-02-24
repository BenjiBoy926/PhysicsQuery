using System;
using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, PhysicsQuery3D.Shape, AdvancedOptions3D>
    {
        [Serializable]
        public class CapsuleShape : Shape
        {
            public readonly struct Position
            {
                public Vector3 Axis => (Cap2 - Cap1) * 0.5f;

                public readonly Vector3 Cap1;
                public readonly Vector3 Cap2;
                public readonly float Radius;

                public Position(Parameters parameters, Axis3D axis, Quaternion rotation, float height, float radius)
                {
                    float extent = height * 0.5f;
                    Vector3 center = parameters.Origin;
                    Vector3 direction = rotation * parameters.TransformDirection(axis.Vector);

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
            private Axis3D _axis = new Axis3D_Y();
            [SerializeField]
            private Quaternion _rotation = Quaternion.identity;
            [SerializeField]
            private float _height = 1;
            [SerializeField]
            private float _radius = 0.5f;

            public CapsuleShape()
            {
            }
            public CapsuleShape(Axis3D axis, Quaternion rotation, float height, float radius)
            {
                _axis = axis;
                _rotation = rotation;
                _height = height;
                _radius = radius;
            }

            public override bool Cast(Parameters parameters, out RaycastHit hit)
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
            public override Result<RaycastHit> CastNonAlloc(Parameters parameters)
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
            public override bool Check(Parameters parameters)
            {
                Position position = GetPosition(parameters);
                return Physics.CheckCapsule(
                    position.Cap1,
                    position.Cap2,
                    position.Radius,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
            }
            public override Result<Collider> OverlapNonAlloc(Parameters parameters)
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
            public override void DrawOverlapGizmo(Parameters parameters)
            {
                DrawGizmo(parameters, parameters.Origin);
            }
            public override void DrawGizmo(Parameters parameters, Vector3 center)
            {
                Position position = GetPosition(parameters);
                CapsuleGizmo3D.Draw(center, position.Axis, position.Radius);
            }

            public Position GetPosition(Parameters parameters)
            {
                return new(parameters, _axis, _rotation, _height, _radius);
            }
        }
    }
}