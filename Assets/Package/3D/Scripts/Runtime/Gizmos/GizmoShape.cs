using UnityEngine;

namespace PhysicsQuery
{
    public abstract class GizmoShape
    {
        protected const float MaxDistance = 1000;
        protected const float NormalLength = 0.3f;
        public const float HitSphereRadius = NormalLength * 0.2f;

        public abstract Result<RaycastHit> DrawCastGizmos();
        public abstract Result<Collider> DrawOverlapGizmos();
    }
    public abstract class GizmoShape<TQuery> : GizmoShape where TQuery : PhysicsQuery
    {
        protected TQuery Query => _query;

        private readonly TQuery _query;

        public GizmoShape(TQuery query)
        {
            _query = query;
        }

        public override Result<RaycastHit> DrawCastGizmos()
        {
            Result<RaycastHit> result = _query.Cast(ResultSort.Distance);
            DrawCastResults(result);
            return result;
        }
        public override Result<Collider> DrawOverlapGizmos()
        {
            Result<Collider> result = _query.Overlap();
            DrawOverlapResult(result);
            return result;
        }

        private void DrawCastResults(Result<RaycastHit> result)
        {
            if (result.IsEmpty)
            {
                DrawEmptyCastResult();
            }
            else
            {
                DrawNonEmptyCastResult(result);
            }
        }
        private void DrawOverlapResult(Result<Collider> result)
        {
            if (result.IsEmpty)
            {
                DrawEmptyOverlapResult();
            }
            else
            {
                DrawNonEmptyOverlapResult(result);
            }
        }

        private void DrawEmptyCastResult()
        {
            Vector3 start = GetStartPosition();
            Vector3 end = GetEndPosition();
            DrawShape(start, Color.gray);
            DrawShape(end, Color.gray);
            DrawLine(start, end, Color.gray);
        }
        private void DrawNonEmptyCastResult(Result<RaycastHit> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                DrawHitPoint(result[i]);
                DrawShapeAtHit(result[i]);
            }
            DrawCastLine(result.Last);
            DrawShape(GetEndPosition(), Color.gray);
        }
        private void DrawEmptyOverlapResult()
        {
            DrawOverlapShape(Color.gray);
        }
        private void DrawNonEmptyOverlapResult(Result<Collider> result)
        {
            DrawOverlapShape(Color.green);
            // TODO: highlight colliders
        }

        private void DrawShapeAtHit(RaycastHit hit)
        {
            Vector3 center = GetShapeCenter(hit);
            DrawShape(center, Color.green);
        }
        private void DrawHitPoint(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(normal.origin, normal.GetPoint(NormalLength));
            Gizmos.DrawSphere(normal.origin, HitSphereRadius);
        }
        private void DrawCastLine(RaycastHit furthestHit)
        {
            Vector3 start = GetStartPosition();
            Vector3 midpoint = GetWorldRay().GetPoint(furthestHit.distance);
            Vector3 end = GetEndPosition();
            DrawLine(start, midpoint, Color.green);
            DrawLine(midpoint, end, Color.gray);
        }
        protected void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(start, end);
        }

        protected Vector3 GetStartPosition()
        {
            return GetWorldRay().origin;
        }
        protected Vector3 GetEndPosition()
        {
            return GetWorldRay().GetPoint(GetMaxDistance());
        }
        private float GetMaxDistance()
        {
            return Mathf.Min(MaxDistance, _query.MaxDistance);
        }
        private Vector3 GetShapeCenter(RaycastHit hit)
        {
            return GetWorldRay().GetPoint(hit.distance);
        }

        protected virtual Ray GetWorldRay()
        {
            return _query.GetWorldRay();
        }

        protected abstract void DrawShape(Vector3 center, Color color);
        protected abstract void DrawOverlapShape(Color color);
    }
}