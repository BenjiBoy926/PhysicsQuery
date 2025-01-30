using System;
using System.Collections.Generic;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class GizmoShape
    {
        protected const float MaxDistance = 1000;

        protected PhysicsQuery Query => _query;
        private PhysicsQuery _query;

        public void DrawCastGizmos(PhysicsQuery query)
        {
            _query = query;
            bool result = query.Cast(out RaycastHit hit);
            DrawResult(result, hit);
        }
        public void DrawCastNonAllocGizmos(PhysicsQuery query)
        {
            _query = query;
            var result = query.CastNonAlloc(ResultSort.Distance);
            DrawResult(result);
        }
        public void DrawCheckGizmos(PhysicsQuery query)
        {
            _query = query;
            bool result = query.Check();
            DrawResult(result);
        }
        public void DrawOverlapNonAllocGizmos(PhysicsQuery query)
        {
            _query = query;
            var result = query.OverlapNonAlloc();
            DrawResult(result);
        }

        private void DrawResult(bool didHit, RaycastHit hit)
        {
            Vector3 start = GetStartPosition();
            if (didHit)
            {
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start, GetShapeCenter(hit));
                Query.DrawGizmo(start);
                DrawHit(hit);
            }
            else
            {
                Gizmos.color = Preferences.MissColor.Value;
                Vector3 end = GetEndPosition();
                Query.DrawGizmo(start);
                Query.DrawGizmo(end);
                Gizmos.DrawLine(start, end);
            }
        }
        private void DrawResult(Result<RaycastHit> result)
        {
            Color hitShapeColor = result.IsFull ? Preferences.CacheFullColor.Value : Preferences.HitColor.Value;

            Gizmos.color = Preferences.GetColorForResult(result);
            Query.DrawGizmo(GetStartPosition());
            for (int i = 0; i < result.Count; i++)
            {
                RaycastHit hit = result[i];
                Gizmos.color = hitShapeColor;
                DrawHit(hit);
            }
            DrawResultLine(result);

            Gizmos.color = Preferences.MissColor.Value;
            Query.DrawGizmo(GetEndPosition());
        }
        private void DrawResult(bool check)
        {
            Gizmos.color = check ? Preferences.HitColor.Value : Preferences.MissColor.Value;
            Query.DrawOverlapGizmo();
        }
        private void DrawResult(Result<Collider> result)
        {
            Gizmos.color = Preferences.GetColorForResult(result);
            Query.DrawOverlapGizmo();

            Gizmos.color = Preferences.ResultItemColor.Value;
            for (int i = 0; i < result.Count; i++)
            {
                ColliderGizmos.DrawGizmos(result[i]);
            }
        }
        private void DrawHit(RaycastHit hit)
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
        private void DrawResultLine(Result<RaycastHit> result)
        {
            Vector3 start = GetStartPosition();
            Vector3 end = GetEndPosition();

            if (result.IsFull)
            {
                Gizmos.color = Preferences.CacheFullColor.Value;
                Gizmos.DrawLine(start, end);
            }
            else if (result.IsEmpty)
            {
                Gizmos.color = Preferences.MissColor.Value;
                Gizmos.DrawLine(start, end);
            }
            else
            {
                Vector3 midpoint = GetWorldRay().GetPoint(result.Last.distance);
                Gizmos.color = Preferences.HitColor.Value;
                Gizmos.DrawLine(start, midpoint);
                Gizmos.color = Preferences.MissColor.Value;
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
    }
}