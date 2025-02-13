using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace PQuery
{
    public class PhysicsShape2D_Capsule : PhysicsShape2D
    {
        [SerializeField]
        private Vector2 _size = new(1, 2);
        [SerializeField]
        private CapsuleDirection2D _direction = CapsuleDirection2D.Vertical;

        public override bool Cast(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters, out RaycastHit2D hit)
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
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
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
        public override bool Check(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
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
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
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

        public override void DrawOverlapGizmo(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            DrawGizmo(parameters, parameters.Origin);
        }
        public override void DrawGizmo(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters, Vector2 center)
        {
            Gizmos.matrix = GetGizmoTransformMatrix(parameters, center, 0);
            CapsuleGizmo2D.Draw(Vector2.zero, _size, _direction);
            Gizmos.matrix = Matrix4x4.identity;
        }

        public Vector2 GetWorldSize(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            return _size * parameters.LossyScale;
        }
        public float GetWorldAngle(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            return GetTransformAngle(parameters);
        }
    }
}