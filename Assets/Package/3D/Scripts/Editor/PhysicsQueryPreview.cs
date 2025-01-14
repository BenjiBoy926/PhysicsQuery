using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class PhysicsQueryPreview
    {
        private const float MaxDistance = 1000;
        private const float NormalLength = 0.1f;

        public abstract string Label { get; }
        protected PhysicsQuery Query => _query;

        private PhysicsQuery _query;
        private Ray _worldRay;

        public PhysicsQueryPreview(PhysicsQuery query)
        {
            _query = query;
            _worldRay = query.GetWorldRay();
        }

        public abstract void Draw();

        protected void DrawHits(RaycastHit[] hits)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                DrawHit(hits[i]);
            }
        }
        protected void DrawHit(RaycastHit hit)
        {
            Handles.color = Color.green;
            Handles.DrawLine(_worldRay.origin, hit.point);

            Ray normal = new(hit.point, hit.normal);
            Handles.color = Color.red;
            Handles.DrawLine(normal.origin, normal.GetPoint(NormalLength));
        }
        protected void DrawNoHit()
        {
            Vector3 start = _worldRay.origin;
            Vector3 end = _worldRay.GetPoint(GetMaxDistance());
            Handles.color = Color.gray;
            Handles.DrawLine(start, end);
        }
        private float GetMaxDistance()
        {
            return Mathf.Min(MaxDistance, _query.MaxDistance);
        }
    }
}