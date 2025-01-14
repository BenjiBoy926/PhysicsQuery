using UnityEngine;

namespace PhysicsQuery
{
    public class RayQuery : PhysicsQuery
    {
        public override bool Cast(out RaycastHit hit)
        {
            Ray ray = GetWorldRay();
            return Physics.Raycast(ray, out hit, MaxDistance, LayerMask, TriggerInteraction);
        }
        public override int CastNonAlloc(out RaycastHit[] hits)
        {
            Ray ray = GetWorldRay();
            hits = GetHitCache();
            return Physics.RaycastNonAlloc(ray, hits, MaxDistance, LayerMask, TriggerInteraction);
        }
        public override bool Check()
        {
            Ray ray = GetWorldRay();
            return Physics.Linecast(ray.origin, ray.GetPoint(MaxDistance), LayerMask, TriggerInteraction);
        }
        public override int OverlapNonAlloc(out Collider[] overlaps)
        {
            int hitCount = CastNonAlloc(out RaycastHit[] hits);
            overlaps = GetColliderCache();
            for (int i = 0; i < hitCount; i++)
            {
                overlaps[i] = hits[i].collider;
            }
            return hitCount;
        }
    }
}
