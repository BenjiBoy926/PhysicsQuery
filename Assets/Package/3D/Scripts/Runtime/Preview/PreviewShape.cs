using UnityEngine;

namespace PhysicsQuery
{
    public abstract class PreviewShape
    {
        protected const float MaxDistance = 1000;
        protected const float NormalLength = 0.1f;
        protected const float HitSphereRadius = NormalLength * 0.3f;

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
            DrawCastResults(Query.Cast());
        }
        public override void DrawOverlapGizmos()
        {
            DrawOverlapResult(Query.Overlap());
        }

        private void DrawCastResults(PhysicsCastResult result)
        {
            if (result.IsEmpty)
            {
                DrawEmptyCastResult();
            }
            else
            {
                DrawNonEmptyResult(result);
            }
        }
        private void DrawOverlapResult(PhysicsOverlapResult result)
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
        private void DrawNonEmptyResult(PhysicsCastResult result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                DrawHitPoint(result.Get(i));
                DrawShapeAtHit(result.Get(i));
            }
            DrawCastLine(result.FurthestHit);
            DrawShape(GetEndPosition(), Color.gray);
        }
        private void DrawEmptyOverlapResult()
        {
            DrawOverlapShape(Color.gray);
        }
        private void DrawNonEmptyOverlapResult(PhysicsOverlapResult result)
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
            Gizmos.color = Color.red;
            Gizmos.DrawLine(normal.origin, normal.GetPoint(NormalLength));
            Gizmos.DrawWireSphere(normal.origin, HitSphereRadius);
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