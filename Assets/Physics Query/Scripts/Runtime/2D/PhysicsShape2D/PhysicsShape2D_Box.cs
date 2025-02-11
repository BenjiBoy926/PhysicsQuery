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

        public override bool Cast(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, out RaycastHit2D hit)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            hit = Physics2D.BoxCast(
                rayDistance.Start.Unwrap(),
                GetWorldSize(parameters),
                GetWorldAngle(parameters),
                rayDistance.Direction.Unwrap(),
                rayDistance.Distance,
                parameters.LayerMask);
            return hit.collider;
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            int count = Physics2D.BoxCastNonAlloc(
                rayDistance.Start.Unwrap(),
                GetWorldSize(parameters),
                GetWorldAngle(parameters),
                rayDistance.Direction.Unwrap(),
                parameters.HitCache,
                rayDistance.Distance,
                parameters.LayerMask);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return Physics2D.OverlapBox(
                parameters.GetWorldStart().Unwrap(),
                GetWorldSize(parameters),
                GetWorldAngle(parameters),
                parameters.LayerMask);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            int count = Physics2D.OverlapBoxNonAlloc(
                parameters.GetWorldStart().Unwrap(),
                GetWorldSize(parameters),
                GetWorldAngle(parameters),
                parameters.ColliderCache,
                parameters.LayerMask);
            return new(parameters.ColliderCache, count);
        }

        public override void DrawOverlapGizmo(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            DrawGizmo(parameters, parameters.GetWorldStart());
        }
        public override void DrawGizmo(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, VectorWrapper2D center)
        {

        }

        public Vector2 GetWorldSize(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return _size * parameters.LossyScale;
        }
        public float GetWorldAngle(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return _angle + parameters.Space.rotation.eulerAngles.z;
        }
    }
}