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

            Gizmos.color = Color.gray;
            DrawShape(start);
            DrawShape(end);
            Gizmos.DrawLine(start, end);
        }
        private void DrawNonEmptyCastResult(Result<RaycastHit> result)
        {
            Color shapeColor = result.IsFull ? Color.red : Color.green;
            for (int i = 0; i < result.Count; i++)
            {
                RaycastHit hit = result[i];

                Gizmos.color = shapeColor;
                DrawShapeAtHit(hit);

                Gizmos.color = Color.blue;
                DrawHitPoint(hit);
                ColliderGizmos.DrawGizmos(hit.collider);
            }
            DrawCastLine(result);

            if (result.IsFull)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.gray;
            }
            DrawShape(GetEndPosition());
        }
        private void DrawEmptyOverlapResult()
        {
            Gizmos.color = Color.gray;
            DrawOverlapShape();
        }
        private void DrawNonEmptyOverlapResult(Result<Collider> result)
        {
            Gizmos.color = Color.green;
            DrawOverlapShape();

            Gizmos.color = Color.blue;
            for (int i = 0; i < result.Count; i++)
            {
                ColliderGizmos.DrawGizmos(result[i]);
            }
        }

        private void DrawShapeAtHit(RaycastHit hit)
        {
            Vector3 center = GetShapeCenter(hit);
            DrawShape(center);
        }
        private void DrawHitPoint(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.DrawLine(normal.origin, normal.GetPoint(NormalLength));
            Gizmos.DrawSphere(normal.origin, HitSphereRadius);
        }
        private void DrawCastLine(Result<RaycastHit> result)
        {
            Vector3 start = GetStartPosition();
            Vector3 midpoint = GetWorldRay().GetPoint(result.Last.distance);
            Vector3 end = GetEndPosition();
                
            if (result.IsFull)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(start, end);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(start, midpoint);
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(midpoint, end);
            }
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

        protected abstract void DrawShape(Vector3 center);
        protected abstract void DrawOverlapShape();
    }
}