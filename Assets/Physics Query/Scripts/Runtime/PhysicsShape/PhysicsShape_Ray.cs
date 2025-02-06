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

        public override bool Cast(PhysicsQuery query, RayDistance worldRay, out RaycastHit hit)
        {
            return Physics.Raycast(worldRay.Ray, out hit, worldRay.Distance, query.LayerMask, query.TriggerInteraction);
        }
        public override int CastNonAlloc(PhysicsQuery query, RayDistance worldRay, RaycastHit[] cache)
        {
            return Physics.RaycastNonAlloc(worldRay.Ray, cache, worldRay.Distance, query.LayerMask, query.TriggerInteraction);
        }
        public override bool Check(PhysicsQuery query, Vector3 worldOrigin)
        {
            return Physics.Linecast(worldOrigin, query.GetWorldEnd(), query.LayerMask, query.TriggerInteraction);
        }
        public override int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            RayDistance ray = new(worldOrigin, query.GetWorldEnd());
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
            Vector3 start = query.GetWorldStart();
            Vector3 end = query.GetWorldEnd();
            Gizmos.DrawLine(start, end);
        }
        public override void DrawGizmo(PhysicsQuery query, Vector3 center)
        {
            // No shapes to draw for raycasting
        }
    }
}