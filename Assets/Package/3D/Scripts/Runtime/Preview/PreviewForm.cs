using UnityEngine;

namespace PhysicsQuery
{
    public abstract class PreviewForm
    {
        protected const float MaxDistance = 1000;
        protected const float NormalLength = 0.1f;
        protected const float HitSphereRadius = NormalLength * 0.3f;

        public abstract void DrawCastGizmos();
        public abstract void DrawOverlapGizmos();

        protected void DrawHitPoint(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(normal.origin, normal.GetPoint(NormalLength));
            Gizmos.DrawWireSphere(normal.origin, HitSphereRadius);
        }
    }
    public abstract class PreviewForm<TQuery> : PreviewForm where TQuery : PhysicsQuery
    {
        protected TQuery Query => _query;

        private readonly TQuery _query;

        public PreviewForm(TQuery query)
        {
            _query = query;
        }

        protected void DrawCastLine(RaycastHit lastHit)
        {
            Vector3 start = _query.GetWorldOrigin();
            Vector3 midpoint = _query.GetWorldRay().GetPoint(lastHit.distance);
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