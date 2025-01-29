using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public class PhysicsShape_Ray : PhysicsShape
    {
        protected override bool DoPhysicsCast(PhysicsQuery query, Ray worldRay, out RaycastHit hit)
        {
            return Physics.Raycast(worldRay, out hit, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        protected override int DoPhysicsCastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache)
        {
            return Physics.RaycastNonAlloc(worldRay, cache, query.MaxDistance, query.LayerMask, query.TriggerInteraction);
        }
        protected override bool DoPhysicsCheck(PhysicsQuery query, Vector3 worldOrigin)
        {
            Ray ray = new(worldOrigin, query.GetWorldDirection());
            Vector3 end = ray.GetPoint(query.MaxDistance);
            return Physics.Linecast(worldOrigin, end, query.LayerMask, query.TriggerInteraction);
        }
        protected override int DoPhysicsOverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            // Note: it would be easier to invoke Cast here, but worldOrigin comes to us from the expensive use of transform.TransformPoint,
            // so we do not want to TransformPoint a second time
            Ray ray = new(worldOrigin, query.GetWorldDirection());
            RaycastHit[] hitCache = query.GetHitCache();
            int count = DoPhysicsCastNonAlloc(query, ray, hitCache);
            for (int i = 0; i < count; i++)
            {
                cache[i] = hitCache[i].collider;
            }
            return count;
        }

    }
}