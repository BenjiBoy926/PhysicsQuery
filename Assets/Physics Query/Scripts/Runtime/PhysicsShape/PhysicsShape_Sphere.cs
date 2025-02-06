using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Sphere : PhysicsShape
    {
        public float Radius => _radius;

        [SerializeField]
        private float _radius = 0.5f;

        public PhysicsShape_Sphere()
        {
        }
        public PhysicsShape_Sphere(float radius)
        {
            _radius = radius;
        }

        public override bool Cast(PhysicsQuery query, RayDistance worldRay, out RaycastHit hit)
        {
            return Physics.SphereCast(
                worldRay.Ray,
                GetWorldRadius(query),
                out hit,
                worldRay.Distance,
                query.LayerMask,
                query.TriggerInteraction);
        }
        public override int CastNonAlloc(PhysicsQuery query, RayDistance worldRay, RaycastHit[] cache)
        {
            return Physics.SphereCastNonAlloc(
                worldRay.Ray,
                GetWorldRadius(query),
                cache,
                worldRay.Distance,
                query.LayerMask,
                query.TriggerInteraction);
        }
        public override bool Check(PhysicsQuery query, Vector3 worldOrigin)
        {
            return Physics.CheckSphere(
                worldOrigin,
                GetWorldRadius(query),
                query.LayerMask,
                query.TriggerInteraction);
        }
        public override int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            return Physics.OverlapSphereNonAlloc(
                worldOrigin,
                GetWorldRadius(query),
                cache,
                query.LayerMask,
                query.TriggerInteraction);
        }
        public override void DrawOverlapGizmo(PhysicsQuery query)
        {
            DrawGizmo(query, query.GetWorldStart());
        }
        public override void DrawGizmo(PhysicsQuery query, Vector3 center)
        {
            Gizmos.DrawWireSphere(center, GetWorldRadius(query));
        }

        public float GetWorldRadius(PhysicsQuery query)
        {
            if (query.Space == Space.World)
            {
                return _radius;
            }
            Vector3 lossyScale = query.transform.lossyScale;
            float max = Mathf.Max(lossyScale.x, lossyScale.y, lossyScale.z);
            return _radius * max;
        }
    }
}