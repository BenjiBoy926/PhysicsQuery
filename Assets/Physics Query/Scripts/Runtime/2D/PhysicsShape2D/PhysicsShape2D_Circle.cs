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

        public override void DrawOverlapGizmo(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            DrawGizmo(parameters, parameters.Origin);
        }
        public override void DrawGizmo(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters, Vector2 center)
        {
            float radius = GetWorldRadius(parameters);
            Vector2 up = Vector2.up * radius;
            Vector2 right = Vector2.right * radius;
            EllipseGizmo gizmo = new(center, up, right);
            gizmo.Draw();
        }

        public float GetWorldRadius(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            Vector2 scale = parameters.LossyScale;
            return _radius * Mathf.Max(scale.x, scale.y);
        }
    }
}