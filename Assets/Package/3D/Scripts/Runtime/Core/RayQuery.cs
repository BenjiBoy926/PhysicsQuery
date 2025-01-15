using UnityEngine;

namespace PhysicsQuery
{
    public class RayQuery : PhysicsQuery
    {
        public override PhysicsCastResult Cast()
        {
            Ray ray = GetWorldRay();
            RaycastHit[] hits = GetHitCache();
            int count = Physics.RaycastNonAlloc(ray, hits, MaxDistance, LayerMask, TriggerInteraction);
            return new(hits, count);
        }
        public override PhysicsOverlapResult Overlap()
        {
            PhysicsCastResult result = Cast();
            Collider[] overlaps = GetColliderCache();
            for (int i = 0; i < result.Count; i++)
            {
                overlaps[i] = result.Get(i).collider;
            }
            return new(overlaps, result.Count);
        }
    }
}
