using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Circle : PhysicsShape2D
    {
        [SerializeField]
        private float _radius = 0.5f;

        public override bool Cast(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters, out RaycastHit2D hit)
        {
            hit = Physics2D.CircleCast(
                parameters.Origin,
                GetWorldRadius(parameters),
                parameters.Direction,
                parameters.Distance,
                parameters.LayerMask);
            return hit.collider;
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            int count = Physics2D.CircleCastNonAlloc(
                parameters.Origin,
                GetWorldRadius(parameters),
                parameters.Direction,
                parameters.HitCache,
                parameters.Distance,
                parameters.LayerMask);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            return Physics2D.OverlapCircle(
                parameters.Origin,
                GetWorldRadius(parameters),
                parameters.LayerMask);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            int count = Physics2D.OverlapCircleNonAlloc(
                parameters.Origin,
                GetWorldRadius(parameters),
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

        public float GetWorldRadius(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            Vector2 scale = parameters.LossyScale;
            return _radius * Mathf.Max(scale.x, scale.y);
        }
    }
}