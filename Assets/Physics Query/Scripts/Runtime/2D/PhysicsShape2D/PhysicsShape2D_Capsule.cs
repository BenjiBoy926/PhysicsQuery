using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Capsule : PhysicsShape2D
    {
        [SerializeField]
        private Vector2 _size = new(1, 2);
        [SerializeField]
        private CapsuleDirection2D _direction = CapsuleDirection2D.Vertical;

        public override bool Cast(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, out RaycastHit2D hit)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            hit = Physics2D.CapsuleCast(
                rayDistance.Start.Unwrap(),
                GetWorldSize(parameters),
                _direction,
                GetWorldAngle(parameters),
                rayDistance.Direction.Unwrap(),
                rayDistance.Distance,
                parameters.LayerMask);
            return hit.collider;
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            int count = Physics2D.CapsuleCastNonAlloc(
                rayDistance.Start.Unwrap(),
                GetWorldSize(parameters),
                _direction,
                GetWorldAngle(parameters),
                rayDistance.Direction.Unwrap(),
                parameters.HitCache,
                rayDistance.Distance,
                parameters.LayerMask);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return Physics2D.OverlapCapsule(
                parameters.GetWorldStart().Unwrap(),
                GetWorldSize(parameters),
                _direction,
                GetWorldAngle(parameters),
                parameters.LayerMask);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            int count = Physics2D.OverlapCapsuleNonAlloc(
                parameters.GetWorldStart().Unwrap(),
                GetWorldSize(parameters),
                _direction,
                GetWorldAngle(parameters),
                parameters.ColliderCache,
                parameters.LayerMask);
            return new(parameters.ColliderCache, count);
        }

        public override void DrawGizmo(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, VectorWrapper2D center)
        {
            throw new System.NotImplementedException();
        }
        public override void DrawOverlapGizmo(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetWorldSize(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return _size * parameters.LossyScale;
        }
        public float GetWorldAngle(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return parameters.Space.rotation.eulerAngles.z;
        }
    }
}