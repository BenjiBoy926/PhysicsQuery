using UnityEngine;

namespace PhysicsQuery
{
    public class EmptyQuery : PhysicsQuery
    {
        public override int Cast(out RaycastHit[] hits)
        {
            hits = GetHitCache();
            return 0;
        }
        public override int Overlap(out Collider[] overlaps)
        {
            overlaps = GetColliderCache();
            return 0;
        }
    }
}
