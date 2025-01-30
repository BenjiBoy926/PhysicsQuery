using UnityEngine;

namespace PQuery.Editor
{
    public abstract class GizmoPreview
    {
        protected const float MaxDistance = 1000;

        protected PhysicsQuery Query => _query;
        protected PhysicsQuery _query;

        protected void DrawHit(RaycastHit hit)
        {
            DrawShapeAtHit(hit);
            Gizmos.color = Preferences.ResultItemColor.Value;
            DrawHitPointNormal(hit);
            ColliderGizmos.DrawGizmos(hit.collider);
        }
        private void DrawShapeAtHit(RaycastHit hit)
        {
            Vector3 center = GetShapeCenter(hit);
            Query.DrawGizmo(center);
        }
        private void DrawHitPointNormal(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.DrawLine(normal.origin, normal.GetPoint(Preferences.HitNormalLength));
            Gizmos.DrawSphere(normal.origin, Preferences.HitSphereRadius.Value);
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
        protected Vector3 GetShapeCenter(RaycastHit hit)
        {
            return GetWorldRay().GetPoint(hit.distance);
        }
        protected Ray GetWorldRay()
        {
            return _query.GetWorldRay();
        }

        public abstract void DrawGizmos(PhysicsQuery query);
    }
}