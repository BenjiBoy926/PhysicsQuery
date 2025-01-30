using UnityEngine;

namespace PQuery.Editor
{
    public abstract class GizmoPreview
    {
        protected const float MaxDistance = 1000;

        protected void DrawHit(PhysicsQuery query, RaycastHit hit)
        {
            DrawShapeAtHit(query, hit);
            Gizmos.color = Preferences.ResultItemColor.Value;
            DrawHitPointNormal(hit);
            ColliderGizmos.DrawGizmos(hit.collider);
        }
        private void DrawShapeAtHit(PhysicsQuery query, RaycastHit hit)
        {
            Vector3 center = GetShapeCenter(query, hit);
            query.DrawGizmo(center);
        }
        private void DrawHitPointNormal(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.DrawLine(normal.origin, normal.GetPoint(Preferences.HitNormalLength));
            Gizmos.DrawSphere(normal.origin, Preferences.HitSphereRadius.Value);
        }

        protected Vector3 GetStartPosition(PhysicsQuery query)
        {
            return GetWorldRay(query).origin;
        }
        protected Vector3 GetEndPosition(PhysicsQuery query)
        {
            return GetWorldRay(query).GetPoint(GetMaxDistance(query));
        }
        private float GetMaxDistance(PhysicsQuery query)
        {
            return Mathf.Min(MaxDistance, query.MaxDistance);
        }
        protected Vector3 GetShapeCenter(PhysicsQuery query, RaycastHit hit)
        {
            return GetWorldRay(query).GetPoint(hit.distance);
        }
        protected Ray GetWorldRay(PhysicsQuery query)
        {
            return query.GetWorldRay();
        }

        public abstract void DrawGizmos(PhysicsQuery query);
    }
}