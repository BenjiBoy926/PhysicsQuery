using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Ray : PhysicsShape
    {
        public PhysicsShape_Ray()
        {
        }

        public override bool Cast(PhysicsQuery query, Ray worldRay, out RaycastHit hit)
        {
            return Physics.Raycast(worldRay, out hit, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        public override int CastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache)
        {
            return Physics.RaycastNonAlloc(worldRay, cache, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        public override bool Check(PhysicsQuery query, Vector3 worldOrigin)
        {
            Ray ray = new(worldOrigin, query.GetWorldDirection());
            Vector3 end = ray.GetPoint(query.MaxDistance);
            return Physics.Linecast(worldOrigin, end, query.LayerMask, query.TriggerInteraction);
        }
        public override int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            Ray ray = new(worldOrigin, query.GetWorldDirection());
            RaycastHit[] hitCache = query.GetHitCache();
            int count = CastNonAlloc(query, ray, hitCache);
            for (int i = 0; i < count; i++)
            {
                cache[i] = hitCache[i].collider;
            }
            return count;
        }
        public override void DrawOverlapGizmo(PhysicsQuery query)
        {
            Ray worldRay = query.GetWorldRay();
            Vector3 start = worldRay.origin;
            Vector3 end = worldRay.GetPoint(query.MaxDistance);
            Gizmos.DrawLine(start, end);
        }
        public override void DrawGizmo(PhysicsQuery query, Vector3 center)
        {
            // No shapes to draw for raycasting
        }
    }
}