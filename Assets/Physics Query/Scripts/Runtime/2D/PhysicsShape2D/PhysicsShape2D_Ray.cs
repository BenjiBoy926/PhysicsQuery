using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Ray : PhysicsShape2D
    {
        public override bool Cast(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, out RaycastHit2D hit)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            hit = Physics2D.Raycast(
                rayDistance.Start.Unwrap(),
                rayDistance.Direction.Unwrap(),
                rayDistance.Distance,
                parameters.LayerMask);
            return hit.collider;
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            int count = Physics2D.RaycastNonAlloc(
                rayDistance.Start.Unwrap(),
                rayDistance.Direction.Unwrap(),
                parameters.HitCache,
                rayDistance.Distance,
                parameters.LayerMask);
            return new(parameters.HitCache, count);
        }
        public override bool Check(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            RayDistance<VectorWrapper2D, RayWrapper2D> rayDistance = parameters.GetWorldRay();
            return Physics2D.Linecast(
                rayDistance.Start.Unwrap(),
                rayDistance.End.Unwrap(),
                parameters.LayerMask);
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            Result<RaycastHit2D> result = CastNonAlloc(parameters);
            for (int i = 0; i < result.Count; i++)
            {
                parameters.ColliderCache[i] = result[i].collider;
            }
            return new(parameters.ColliderCache, result.Count);
        }

        public override void DrawGizmo(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, VectorWrapper2D center)
        {
            throw new System.NotImplementedException();
        }
        public override void DrawOverlapGizmo(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}