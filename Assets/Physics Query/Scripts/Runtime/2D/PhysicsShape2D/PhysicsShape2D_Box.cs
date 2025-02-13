using System;
using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Box : PhysicsShape2D
    {
        [SerializeField]
        private Vector2 _size = Vector2.one;
        [SerializeField]
        private float _angle = 0;

        public override bool Cast(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters, out RaycastHit2D hit)
        {
            hit = Physics2D.BoxCast(
                parameters.Origin,
                GetWorldSize(parameters),
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
            int count = Physics2D.BoxCast(
                parameters.Origin,
                GetWorldSize(parameters),
                GetWorldAngle(parameters),
                parameters.Direction,
                parameters.Advanced.Filter,
                parameters.HitCache,
                parameters.Distance);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            return Physics2D.OverlapBox(
                parameters.Origin,
                GetWorldSize(parameters),
                GetWorldAngle(parameters),
                parameters.Advanced.LayerMask,
                parameters.Advanced.MinDepth,
                parameters.Advanced.MaxDepth);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            int count = Physics2D.OverlapBox(
                parameters.Origin,
                GetWorldSize(parameters),
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
            Vector2 extents = _size * 0.5f;
            ReadOnlySpan<Vector3> corners = stackalloc Vector3[]
            {
                extents, new(extents.x, -extents.y, 0), -extents, new(-extents.x, extents.y, 0)
            };
            Gizmos.matrix = GetGizmoTransformMatrix(parameters, center, _angle);
            Gizmos.DrawLineStrip(corners, true);
            Gizmos.matrix = Matrix4x4.identity;
        }

        public Vector2 GetWorldSize(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            return _size * parameters.LossyScale;
        }
        public float GetWorldAngle(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            return _angle + GetTransformAngle(parameters);
        }
    }
}