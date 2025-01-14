using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class PreviewForm
    {
        protected const float MaxDistance = 1000;
        protected const float NormalLength = 0.1f;

        public abstract void DrawCast();
        public abstract void DrawOverlap();
    }
    public abstract class PreviewForm<TQuery> : PreviewForm where TQuery : PhysicsQuery
    {
        protected TQuery Query => _query;

        private TQuery _query;

        public PreviewForm(TQuery query)
        {
            _query = query;
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