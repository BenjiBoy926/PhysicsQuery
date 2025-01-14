using UnityEngine;

namespace PhysicsQuery
{
    public class RayQuery : PhysicsQuery
    {
        public override int Cast(out RaycastHit[] hits)
        {
            Ray ray = GetWorldRay();
            hits = GetHitCache();
            return Physics.RaycastNonAlloc(ray, hits, MaxDistance, LayerMask, TriggerInteraction);
        }
        public override int Overlap(out Collider[] overlaps)
        {
            int hitCount = Cast(out RaycastHit[] hits);
            overlaps = GetColliderCache();
            for (int i = 0; i < hitCount; i++)
            {
                overlaps[i] = hits[i].collider;
            }
            return hitCount;
        }
    }
}
