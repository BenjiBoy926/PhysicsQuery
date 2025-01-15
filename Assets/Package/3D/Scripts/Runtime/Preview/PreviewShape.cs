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
    }
    public abstract class PreviewShape<TQuery> : PreviewShape where TQuery : PhysicsQuery
    {
        protected TQuery Query => _query;

        private readonly TQuery _query;

        public PreviewShape(TQuery query)
        {
            _query = query;
        }

        protected void DrawDefaultLine()
        {
            DrawDefaultLine(Color.gray);
        }
        protected void DrawDefaultLine(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(_query.GetWorldOrigin(), GetEndPoint());
        }

        protected void DrawCastResults(PhysicsCastResult result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                DrawHitPoint(result.Get(i));
            }
            DrawCastLine(result.FurthestHit);
        }
        protected void DrawHitPoint(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(normal.origin, normal.GetPoint(NormalLength));
            Gizmos.DrawWireSphere(normal.origin, HitSphereRadius);
        }
        protected void DrawCastLine(RaycastHit furthestHit)
        {
            Vector3 start = _query.GetWorldOrigin();
            Vector3 midpoint = _query.GetWorldRay().GetPoint(furthestHit.distance);
            Vector3 end = GetEndPoint();

            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, midpoint);

            Gizmos.color = Color.gray;
            Gizmos.DrawLine(midpoint, end);
        }

        protected Vector3 GetEndPoint()
        {
            return Query.GetWorldRay().GetPoint(GetMaxDistance());
        }
        private float GetMaxDistance()
        {
            return Mathf.Min(MaxDistance, Query.MaxDistance);
        }
    }
}