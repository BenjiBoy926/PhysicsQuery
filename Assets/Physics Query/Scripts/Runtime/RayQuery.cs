using UnityEngine;

namespace PQuery
{
    public class RayQuery : PhysicsQuery
    {
        protected override bool DoPhysicsCast(Ray worldRay, out RaycastHit hit)
        {
            return Physics.Raycast(worldRay, out hit, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override int DoPhysicsCastNonAlloc(Ray worldRay, RaycastHit[] cache)
        {
            return Physics.RaycastNonAlloc(worldRay, cache, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override bool DoPhysicsCheck(Vector3 worldOrigin)
        {
            Ray ray = new(worldOrigin, GetWorldDirection());
            Vector3 end = ray.GetPoint(MaxDistance);
            return Physics.Linecast(worldOrigin, end, LayerMask, TriggerInteraction);
        }
        protected override int DoPhysicsOverlapNonAlloc(Vector3 worldOrigin, Collider[] cache)
        {
            // Note: it would be easier to invoke Cast here, but worldOrigin comes to us from the expensive use of transform.TransformPoint,
            // so we do not want to TransformPoint a second time
            Ray ray = new(worldOrigin, GetWorldDirection());
            RaycastHit[] hitCache = GetHitCache();
            int count = DoPhysicsCastNonAlloc(ray, hitCache);
            for (int i = 0; i < count; i++)
            {
                cache[i] = hitCache[i].collider;
            }
            return count;
        }
    }
}
