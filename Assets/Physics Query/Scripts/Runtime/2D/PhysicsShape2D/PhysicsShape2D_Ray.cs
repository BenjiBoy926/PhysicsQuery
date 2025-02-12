using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Ray : PhysicsShape2D
    {
        public override bool Cast(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters, out RaycastHit2D hit)
        {
            hit = Physics2D.Raycast(
                parameters.Origin,
                parameters.Direction,
                parameters.Distance,
                parameters.LayerMask);
            return hit.collider;
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            int count = Physics2D.RaycastNonAlloc(
                parameters.Origin,
                parameters.Direction,
                parameters.HitCache,
                parameters.Distance,
                parameters.LayerMask);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            return Physics2D.Linecast(
                parameters.Origin,
                GetEnd(parameters),
                parameters.LayerMask);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            Result<RaycastHit2D> result = CastNonAlloc(parameters);
            for (int i = 0; i < result.Count; i++)
            {
                parameters.ColliderCache[i] = result[i].collider;
            }
            return new(parameters.ColliderCache, result.Count);
        }

        public override void DrawOverlapGizmo(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            Vector2 start = parameters.Origin;
            Vector2 end = GetEnd(parameters);
            Gizmos.DrawLine(start, end);
        }
        public override void DrawGizmo(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters, Vector2 center)
        {

        }

        public Vector2 GetEnd(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            return GetRay(parameters).GetPoint(parameters.Distance);
        }
        public Ray2D GetRay(PhysicsParameters<Vector2, RaycastHit2D, Collider2D> parameters)
        {
            return new(parameters.Origin, parameters.Direction);
        }
    }
}