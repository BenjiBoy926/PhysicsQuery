using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Circle : PhysicsShape2D
    {
        [SerializeField]
        private float _radius = 0.5f;

        public override bool Cast(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, out RaycastHit2D hit)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            hit = Physics2D.CircleCast(
                rayDistance.Start.Unwrap(),
                GetWorldRadius(parameters),
                rayDistance.Direction.Unwrap(),
                rayDistance.Distance,
                parameters.LayerMask);
            return hit.collider;
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            int count = Physics2D.CircleCastNonAlloc(
                rayDistance.Start.Unwrap(),
                GetWorldRadius(parameters),
                rayDistance.Direction.Unwrap(),
                parameters.HitCache,
                rayDistance.Distance,
                parameters.LayerMask);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return Physics2D.OverlapCircle(
                parameters.GetWorldStart().Unwrap(),
                GetWorldRadius(parameters),
                parameters.LayerMask);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            int count = Physics2D.OverlapCircleNonAlloc(
                parameters.GetWorldStart().Unwrap(),
                GetWorldRadius(parameters),
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

        public float GetWorldRadius(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            Vector2 scale = parameters.LossyScale;
            return _radius * Mathf.Max(scale.x, scale.y);
        }
    }
}