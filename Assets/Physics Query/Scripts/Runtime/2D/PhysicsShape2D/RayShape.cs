using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery2D : PhysicsQueryGeneric<Vector2, RaycastHit2D, Collider2D, ResultSort2D, PhysicsQuery2D.Shape, AdvancedOptions2D>
    {
        public class RayShape : Shape
        {
            public override bool Cast(Parameters parameters, out RaycastHit2D hit)
            {
                hit = Physics2D.Raycast(
                    parameters.Origin,
                    parameters.Direction,
                    parameters.Distance,
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.MinDepth,
                    parameters.Advanced.MaxDepth);
                return hit.collider;
            }
            public override Result<RaycastHit2D> CastNonAlloc(Parameters parameters)
            {
                int count = Physics2D.Raycast(
                    parameters.Origin,
                    parameters.Direction,
                    parameters.Advanced.Filter,
                    parameters.HitCache,
                    parameters.Distance);
                return new(parameters.HitCache, count);
            }
            public override bool Check(Parameters parameters)
            {
                return Physics2D.Linecast(
                    parameters.Origin,
                    GetEnd(parameters),
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.MinDepth,
                    parameters.Advanced.MaxDepth);
            }
            public override Result<Collider2D> OverlapNonAlloc(Parameters parameters)
            {
                Result<RaycastHit2D> result = CastNonAlloc(parameters);
                for (int i = 0; i < result.Count; i++)
                {
                    parameters.ColliderCache[i] = result[i].collider;
                }
                return new(parameters.ColliderCache, result.Count);
            }

            public override void DrawOverlapGizmo(Parameters parameters)
            {
                Vector2 start = parameters.Origin;
                Vector2 end = GetEnd(parameters);
                Gizmos.DrawLine(start, end);
            }
            public override void DrawGizmo(Parameters parameters, Vector2 center)
            {

            }

            public Vector2 GetEnd(Parameters parameters)
            {
                return GetRay(parameters).GetPoint(parameters.Distance);
            }
            public Ray2D GetRay(Parameters parameters)
            {
                return new(parameters.Origin, parameters.Direction);
            }
        }
    }
}