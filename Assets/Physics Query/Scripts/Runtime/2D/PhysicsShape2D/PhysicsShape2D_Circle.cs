using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Circle : PhysicsShape2D
    {
        [SerializeField]
        private float _radius = 0.5f;

        public override bool Cast(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters, out RaycastHit2D hit)
        {
            hit = Physics2D.CircleCast(
                parameters.Origin,
                GetWorldRadius(parameters),
                parameters.Direction,
                parameters.Distance,
                parameters.Advanced.LayerMask,
                parameters.Advanced.MinDepth,
                parameters.Advanced.MaxDepth);
            return hit.collider;
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            int count = Physics2D.CircleCast(
                parameters.Origin,
                GetWorldRadius(parameters),
                parameters.Direction,
                parameters.Advanced.Filter,
                parameters.HitCache,
                parameters.Distance);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            return Physics2D.OverlapCircle(
                parameters.Origin,
                GetWorldRadius(parameters),
                parameters.Advanced.LayerMask,
                parameters.Advanced.MinDepth,
                parameters.Advanced.MaxDepth);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            int count = Physics2D.OverlapCircle(
                parameters.Origin,
                GetWorldRadius(parameters),
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
            float radius = GetWorldRadius(parameters);
            CircleGizmo2D.Draw(center, radius);
        }

        public float GetWorldRadius(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            Vector2 scale = parameters.LossyScale;
            return _radius * Mathf.Max(scale.x, scale.y);
        }
    }
}