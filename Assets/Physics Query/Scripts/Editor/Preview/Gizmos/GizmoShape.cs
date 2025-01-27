using System;
using System.Collections.Generic;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class GizmoShape
    {
        protected const float MaxDistance = 1000;

        private static readonly Dictionary<Type, GizmoShape> _queryTypeToGizmoShape = new()
        {
            { typeof(BoxQuery), new GizmoShape_Box() },
            { typeof(CapsuleQuery), new GizmoShape_Capsule() },
            { typeof(EmptyQuery), new GizmoShape_Empty() },
            { typeof(RayQuery), new GizmoShape_Ray() },
            { typeof(SphereQuery), new GizmoShape_Sphere() }
        };

        public static GizmoShape Get(PhysicsQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            Type queryType = query.GetType();
            if (!_queryTypeToGizmoShape.ContainsKey(queryType))
            {
                throw new NotImplementedException($"Query type '{queryType}' has no gizmo shape defined");
            }
            return _queryTypeToGizmoShape[queryType];
        }

        public abstract void DrawCastGizmos(PhysicsQuery query);
        public abstract void DrawOverlapGizmos(PhysicsQuery query);
    }
    public abstract class GizmoShape<TQuery> : GizmoShape where TQuery : PhysicsQuery
    {
        protected TQuery Query => _query;
        private TQuery _query;

        public override void DrawCastGizmos(PhysicsQuery query)
        {
            _query = (TQuery)query;
            var result = query.CastNonAlloc(ResultSort.Distance);
            DrawResult(result);
        }
        public override void DrawOverlapGizmos(PhysicsQuery query)
        {
            _query = (TQuery)query;
            var result = query.OverlapNonAlloc();
            DrawResult(result);
        }

        private void DrawResult(Result<RaycastHit> result)
        {
            Color hitShapeColor = result.IsFull ? Preferences.CacheFullColor.Value : Preferences.HitColor.Value;

            Gizmos.color = Preferences.GetColorForResult(result);
            DrawShape(GetStartPosition());
            for (int i = 0; i < result.Count; i++)
            {
                RaycastHit hit = result[i];

                Gizmos.color = hitShapeColor;
                DrawShapeAtHit(hit);

                Gizmos.color = Preferences.ResultItemColor.Value;
                DrawHitPoint(hit);
                ColliderGizmos.DrawGizmos(hit.collider);
            }
            DrawCastLine(result);

            if (result.IsFull)
            {
                Gizmos.color = Preferences.CacheFullColor.Value;
            }
            else
            {
                Gizmos.color = Preferences.MissColor.Value;
            }
            DrawShape(GetEndPosition());
        }
        private void DrawResult(Result<Collider> result)
        {
            Gizmos.color = Preferences.GetColorForResult(result);
            DrawOverlapShape();

            Gizmos.color = Preferences.ResultItemColor.Value;
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
            Gizmos.DrawSphere(normal.origin, Preferences.HitSphereRadius.Value);
        }
        private void DrawCastLine(Result<RaycastHit> result)
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

        protected abstract void DrawShape(Vector3 center);
        protected abstract void DrawOverlapShape();
    }
}