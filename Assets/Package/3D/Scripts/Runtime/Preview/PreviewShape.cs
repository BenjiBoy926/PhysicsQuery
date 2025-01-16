using UnityEngine;

namespace PhysicsQuery
{
    public abstract class PreviewShape
    {
        protected const float MaxDistance = 1000;
        protected const float NormalLength = 0.3f;
        protected const float HitSphereRadius = NormalLength * 0.2f;

        public CastResult CastResult
        {
            get => _castResult;
            protected set => _castResult = value;
        }
        public OverlapResult OverlapResult
        {
            get => _overlapResult;
            protected set => _overlapResult = value;
        }

        private CastResult _castResult;
        private OverlapResult _overlapResult;

        public abstract void DrawCastGizmos();
        public abstract void DrawOverlapGizmos();

        protected abstract void DrawShape(Vector3 center, Color color);
        protected abstract void DrawOverlapShape(Color color);
    }
    public abstract class PreviewShape<TQuery> : PreviewShape where TQuery : PhysicsQuery
    {
        protected TQuery Query => _query;

        private readonly TQuery _query;

        public PreviewShape(TQuery query)
        {
            _query = query;
        }

        public override void DrawCastGizmos()
        {
            CastResult = Query.Cast(ResultSort.Distance);
            DrawCastResults(CastResult);
        }
        public override void DrawOverlapGizmos()
        {
            OverlapResult = Query.Overlap();
            DrawOverlapResult(OverlapResult);
        }

        private void DrawCastResults(CastResult result)
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
        private void DrawOverlapResult(OverlapResult result)
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
        private void DrawNonEmptyCastResult(CastResult result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                DrawHitPoint(result.Get(i));
                DrawShapeAtHit(result.Get(i));
            }
            DrawCastLine(result.Last);
            DrawShape(GetEndPosition(), Color.gray);
        }
        private void DrawEmptyOverlapResult()
        {
            DrawOverlapShape(Color.gray);
        }
        private void DrawNonEmptyOverlapResult(OverlapResult result)
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
            return Mathf.Min(MaxDistance, Query.MaxDistance);
        }
        private Vector3 GetShapeCenter(RaycastHit hit)
        {
            return GetWorldRay().GetPoint(hit.distance);
        }

        protected virtual Ray GetWorldRay()
        {
            return Query.GetWorldRay();
        }
    }
}