using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Capsule : PhysicsShape2D
    {
        [SerializeField]
        private Vector2 _size = new(1, 2);
        [SerializeField]
        private CapsuleDirection2D _direction = CapsuleDirection2D.Vertical;

        public override bool Cast(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters, out RaycastHit2D hit)
        {
            hit = Physics2D.CapsuleCast(
                parameters.Origin,
                GetWorldSize(parameters),
                _direction,
                GetWorldAngle(parameters),
                parameters.Direction,
                parameters.Distance,
                parameters.LayerMask);
            return hit.collider;
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            int count = Physics2D.CapsuleCastNonAlloc(
                parameters.Origin,
                GetWorldSize(parameters),
                _direction,
                GetWorldAngle(parameters),
                parameters.Direction,
                parameters.HitCache,
                parameters.Distance,
                parameters.LayerMask);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            return Physics2D.OverlapCapsule(
                parameters.Origin,
                GetWorldSize(parameters),
                _direction,
                GetWorldAngle(parameters),
                parameters.LayerMask);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            int count = Physics2D.OverlapCapsuleNonAlloc(
                parameters.Origin,
                GetWorldSize(parameters),
                _direction,
                GetWorldAngle(parameters),
                parameters.ColliderCache,
                parameters.LayerMask);
            return new(parameters.ColliderCache, count);
        }

        public override void DrawGizmo(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters, Vector2 center)
        {
            throw new System.NotImplementedException();
        }
        public override void DrawOverlapGizmo(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetWorldSize(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            return _size * parameters.LossyScale;
        }
        public float GetWorldAngle(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            return parameters.Space.rotation.eulerAngles.z;
        }
    }
}