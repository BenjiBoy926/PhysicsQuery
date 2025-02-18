using UnityEngine;

namespace PQuery.Editor
{
    public abstract class GizmoPreview
    {
        protected void DrawShapeAtHit(PhysicsQuery query, AgnosticRaycastHit hit)
        {
            Vector3 center = GetShapeCenter(query, hit);
            query.DrawGizmo(center);
        }
        protected void DrawHit(AgnosticRaycastHit hit)
        {
            Gizmos.color = Preferences.ResultItemColor.Value;
            DrawHitPointNormal(hit);
            ColliderGizmos.DrawGizmos(hit.Collider);
        }
        private void DrawHitPointNormal(AgnosticRaycastHit hit)
        {
            Ray normal = new(hit.Point, hit.Normal);
            Gizmos.DrawLine(normal.origin, normal.GetPoint(Preferences.HitNormalLength));
            Gizmos.DrawSphere(normal.origin, Preferences.HitSphereRadius.Value);
        }

        protected Vector3 GetShapeCenter(PhysicsQuery query, AgnosticRaycastHit hit)
        {
            return query.GetAgnosticWorldRay().GetPoint(hit.Distance);
        }

        public abstract void DrawGizmos(PhysicsQuery query);
    }
}