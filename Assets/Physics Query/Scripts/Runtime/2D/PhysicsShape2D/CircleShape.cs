using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery2D : PhysicsQueryGeneric<Vector2, RaycastHit2D, Collider2D, PhysicsQuery2D.Shape, AdvancedOptions2D>
    {
        public class CircleShape : Shape
        {
            [SerializeField]
            private float _radius = 0.5f;

            public override bool Cast(Parameters parameters, out RaycastHit2D hit)
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
            public override Result<RaycastHit2D> CastNonAlloc(Parameters parameters)
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
            public override bool Check(Parameters parameters)
            {
                return Physics2D.OverlapCircle(
                    parameters.Origin,
                    GetWorldRadius(parameters),
                    parameters.Advanced.LayerMask,
                    parameters.Advanced.MinDepth,
                    parameters.Advanced.MaxDepth);
            }
            public override Result<Collider2D> OverlapNonAlloc(Parameters parameters)
            {
                int count = Physics2D.OverlapCircle(
                    parameters.Origin,
                    GetWorldRadius(parameters),
                    parameters.Advanced.Filter,
                    parameters.ColliderCache);
                return new(parameters.ColliderCache, count);
            }

            public override void DrawOverlapGizmo(Parameters parameters)
            {
                DrawGizmo(parameters, parameters.Origin);
            }
            public override void DrawGizmo(Parameters parameters, Vector2 center)
            {
                float radius = GetWorldRadius(parameters);
                CircleGizmo2D.Draw(center, radius);
            }

            public float GetWorldRadius(Parameters parameters)
            {
                Vector2 scale = parameters.LossyScale;
                return _radius * Mathf.Max(scale.x, scale.y);
            }
        }
    }
}