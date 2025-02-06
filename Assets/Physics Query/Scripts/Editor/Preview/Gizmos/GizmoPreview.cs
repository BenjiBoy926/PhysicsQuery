using UnityEngine;

namespace PQuery.Editor
{
    public abstract class GizmoPreview
    {
        protected void DrawShapeAtHit(PhysicsQuery query, RaycastHit hit)
        {
            Vector3 center = GetShapeCenter(query, hit);
            query.DrawGizmo(center);
        }
        protected void DrawHit(PhysicsQuery query, RaycastHit hit)
        {
            Gizmos.color = Preferences.ResultItemColor.Value;
            DrawHitPointNormal(hit);
            ColliderGizmos.DrawGizmos(hit.collider);
        }
        private void DrawHitPointNormal(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.DrawLine(normal.origin, normal.GetPoint(Preferences.HitNormalLength));
            Gizmos.DrawSphere(normal.origin, Preferences.HitSphereRadius.Value);
        }

        protected Vector3 GetShapeCenter(PhysicsQuery query, RaycastHit hit)
        {
            return query.GetWorldRay().GetPoint(hit.distance);
        }

        public abstract void DrawGizmos(PhysicsQuery query);
    }
}