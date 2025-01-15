using UnityEngine;

namespace PhysicsQuery
{
    public class RayQuery : PhysicsQuery
    {
        protected override int PerformCast(Ray worldRay, RaycastHit[] cache)
        {
            return Physics.RaycastNonAlloc(worldRay, cache, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override int PerformOverlap(Vector3 worldOrigin, Collider[] cache)
        {
            Ray ray = new(worldOrigin, GetWorldDirection());
            RaycastHit[] hitCache = GetHitCache();
            int count = PerformCast(ray, hitCache);
            for (int i = 0; i < count; i++)
            {
                cache[i] = hitCache[i].collider;
            }
            return count;
        }
    }
}
