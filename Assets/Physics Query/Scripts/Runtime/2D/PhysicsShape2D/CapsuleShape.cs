using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery2D : PhysicsQueryGeneric<Vector2, RaycastHit2D, Collider2D, PhysicsQuery2D.Shape, AdvancedOptions2D>
    {
        public class CapsuleShape : Shape
        {
            [SerializeField]
            private Vector2 _size = new(1, 2);
            [SerializeField]
            private CapsuleDirection2D _direction = CapsuleDirection2D.Vertical;

            public override bool Cast(Parameters parameters, out RaycastHit2D hit)
            {
                hit = Physics2D.CapsuleCast(
                    parameters.Origin,
                    GetWorldSize(parameters),
                    _direction,
                    GetWorldAngle(parameters),
                    parameters.Direction,
                    parameters.Distance,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.MinDepth,
                    parameters.Advanced.MaxDepth);
                return hit.collider;
            }
            public override Result<RaycastHit2D> CastNonAlloc(Parameters parameters)
            {
                int count = Physics2D.CapsuleCast(
                    parameters.Origin,
                    GetWorldSize(parameters),
                    _direction,
                    GetWorldAngle(parameters),
                    parameters.Direction,
                    parameters.Advanced.Filter,
                    parameters.HitCache,
                    parameters.Distance);
                return new(parameters.HitCache, count);
            }
            public override bool Check(Parameters parameters)
            {
                return Physics2D.OverlapCapsule(
                    parameters.Origin,
                    GetWorldSize(parameters),
                    _direction,
                    GetWorldAngle(parameters),
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.MinDepth,
                    parameters.Advanced.MaxDepth);
            }
            public override Result<Collider2D> OverlapNonAlloc(Parameters parameters)
            {
                int count = Physics2D.OverlapCapsule(
                    parameters.Origin,
                    GetWorldSize(parameters),
                    _direction,
                    GetWorldAngle(parameters),
                    parameters.Advanced.Filter,
                    parameters.ColliderCache);
                return new(parameters.ColliderCache, count);
            }

            public override void DrawOverlapGizmo(Parameters parameters)
            {
                DrawGizmo(parameters, parameters.Origin);
            }
            public override void DrawGizmo(Parameters parameters, Vector3 center)
            {
                Matrix4x4 transformation = GetProjectedTransformation(parameters, center, 0);
                CapsuleGizmo2D.Draw(transformation, Vector2.zero, _size, _direction);
            }

            public Vector2 GetWorldSize(Parameters parameters)
            {
                Matrix4x4 transformation = GetProjectedTransformation(parameters, Vector3.zero, 0);
                return CapsuleMath2D.TransformSize(transformation, _size);
            }
            public float GetWorldAngle(Parameters parameters)
            {
                return GetTransformAngle(parameters);
            }
        }
    }
}