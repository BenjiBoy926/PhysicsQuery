using System;
using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, ResultSort3D, PhysicsQuery3D.Shape, AdvancedOptions3D>
    {
        [Serializable]
        public class BoxShape : Shape
        {
            [SerializeField]
            private Vector3 _size = Vector3.one;
            [SerializeField]
            private Quaternion _orientation = Quaternion.identity;

            public BoxShape()
            {
            }
            public BoxShape(Vector3 size, Quaternion orientation)
            {
                _size = size;
                _orientation = orientation;
            }

            public override bool Cast(Parameters parameters, out RaycastHit hit)
            {
                return Physics.BoxCast(
                    parameters.Origin,
                    GetWorldExtents(parameters),
                    parameters.Direction,
                    out hit,
                    GetWorldOrientation(parameters),
                    parameters.Distance,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
            }
            public override Result<RaycastHit> CastNonAlloc(Parameters parameters)
            {
                int count = Physics.BoxCastNonAlloc(
                    parameters.Origin,
                    GetWorldExtents(parameters),
                    parameters.Direction,
                    parameters.HitCache,
                    GetWorldOrientation(parameters),
                    parameters.Distance,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
                return new(parameters.HitCache, count);
            }
            public override bool Check(Parameters parameters)
            {
                return Physics.CheckBox(
                    parameters.Origin,
                    GetWorldExtents(parameters),
                    GetWorldOrientation(parameters),
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.TriggerInteraction);
            }
            public override Result<Collider> OverlapNonAlloc(Parameters parameters)
            {
                int count = Physics.OverlapBoxNonAlloc(
                    parameters.Origin,
                    GetWorldExtents(parameters),
                    parameters.ColliderCache,
                    GetWorldOrientation(parameters),
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
                Gizmos.matrix = Matrix4x4.TRS(center, GetWorldOrientation(parameters), parameters.LossyScale);
                Gizmos.DrawWireCube(Vector3.zero, _size);
                Gizmos.matrix = Matrix4x4.identity;
            }

            public Vector3 GetWorldExtents(Parameters parameters)
            {
                return GetWorldSize(parameters) * 0.5f;
            }
            public Vector3 GetWorldSize(Parameters parameters)
            {
                return parameters.TransformScale(_size);
            }
            public Quaternion GetWorldOrientation(Parameters parameters)
            {
                return parameters.TransformRotation(_orientation);
            }
        }
    }
}