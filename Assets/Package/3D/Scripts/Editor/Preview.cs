using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class Preview
    {
        private const float MaxDistance = 1000;
        private const float NormalLength = 0.1f;

        public abstract string Label { get; }
        protected PhysicsQuery Query => _query;
        protected Ray WorldRay => _query.GetWorldRay();

        private readonly PhysicsQuery _query;

        public Preview(PhysicsQuery query)
        {
            _query = query;
        }

        public abstract void Draw();

        protected void DrawHits(RaycastHit[] hits, int count)
        {
            for (int i = 0; i < count; i++)
            {
                DrawHit(hits[i]);
            }
        }
        protected void DrawHit(RaycastHit hit)
        {
            Handles.color = Color.green;
            Handles.DrawLine(WorldRay.origin, hit.point);

            Ray normal = new(hit.point, hit.normal);
            Handles.color = Color.red;
            Handles.DrawLine(normal.origin, normal.GetPoint(NormalLength));
        }
        protected void DrawNoHit(Vector3 start)
        {
            Vector3 end = WorldRay.GetPoint(GetMaxDistance());
            Handles.color = Color.gray;
            Handles.DrawLine(start, end);
        }
        private float GetMaxDistance()
        {
            return Mathf.Min(MaxDistance, _query.MaxDistance);
        }
    }
}