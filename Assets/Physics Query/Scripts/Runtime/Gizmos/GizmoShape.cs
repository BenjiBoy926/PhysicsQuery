using UnityEngine;

namespace PhysicsQuery
{
    public abstract class GizmoShape
    {
        protected const float MaxDistance = 1000;

        public abstract void DrawCastGizmos(Result<RaycastHit> result);
        public abstract void DrawOverlapGizmos(Result<Collider> result);
    }
    public abstract class GizmoShape<TQuery> : GizmoShape where TQuery : PhysicsQuery
    {
        protected TQuery Query => _query;

        private readonly TQuery _query;

        public GizmoShape(TQuery query)
        {
            _query = query;
        }

        public override void DrawCastGizmos(Result<RaycastHit> result)
        {
            Color hitShapeColor = result.IsFull ? Preferences.CacheFullColor : Preferences.HitColor;

            Gizmos.color = Preferences.GetColorForResult(result);
            DrawShape(GetStartPosition());
            for (int i = 0; i < result.Count; i++)
            {
                RaycastHit hit = result[i];

                Gizmos.color = hitShapeColor;
                DrawShapeAtHit(hit);

                Gizmos.color = Preferences.ResultItemColor;
                DrawHitPoint(hit);
                ColliderGizmos.DrawGizmos(hit.collider);
            }
            DrawCastLine(result);

            if (result.IsFull)
            {
                Gizmos.color = Preferences.CacheFullColor;
            }
            else
            {
                Gizmos.color = Preferences.MissColor;
            }
            DrawShape(GetEndPosition());
        }
        public override void DrawOverlapGizmos(Result<Collider> result)
        {
            Gizmos.color = Preferences.GetColorForResult(result);
            DrawOverlapShape();

            Gizmos.color = Preferences.ResultItemColor;
            for (int i = 0; i < result.Count; i++)
            {
                ColliderGizmos.DrawGizmos(result[i]);
            }
        }

        private void DrawShapeAtHit(RaycastHit hit)
        {
            Vector3 center = GetShapeCenter(hit);
            DrawShape(center);
        }
        private void DrawHitPoint(RaycastHit hit)
        {
            Ray normal = new(hit.point, hit.normal);
            Gizmos.DrawLine(normal.origin, normal.GetPoint(Preferences.HitNormalLength));
            Gizmos.DrawSphere(normal.origin, Preferences.HitSphereRadius);
        }
        private void DrawCastLine(Result<RaycastHit> result)
        {
            Vector3 start = GetStartPosition();
            Vector3 end = GetEndPosition();
                
            if (result.IsFull)
            {
                Gizmos.color = Preferences.CacheFullColor;
                Gizmos.DrawLine(start, end);
            }
            else if (result.IsEmpty)
            {
                Gizmos.color = Preferences.MissColor;
                Gizmos.DrawLine(start, end);
            }
            else
            {
                Vector3 midpoint = GetWorldRay().GetPoint(result.Last.distance);
                Gizmos.color = Preferences.HitColor;
                Gizmos.DrawLine(start, midpoint);
                Gizmos.color = Preferences.MissColor;
                Gizmos.DrawLine(midpoint, end);
            }
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
        private Vector3 GetShapeCenter(RaycastHit hit)
        {
            return GetWorldRay().GetPoint(hit.distance);
        }
        protected virtual Ray GetWorldRay()
        {
            return _query.GetWorldRay();
        }

        protected abstract void DrawShape(Vector3 center);
        protected abstract void DrawOverlapShape();
    }
}