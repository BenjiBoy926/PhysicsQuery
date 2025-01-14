using UnityEngine;

namespace PhysicsQuery
{
    public class EmptyQuery : PhysicsQuery
    {
        public override bool Cast(out RaycastHit hit)
        {
            hit = new RaycastHit();
            return false;
        }
        public override int CastNonAlloc(out RaycastHit[] hits)
        {
            hits = GetHitCache();
            return 0;
        }
        public override bool Check()
        {
            return false;
        }
        public override int OverlapNonAlloc(out Collider[] overlaps)
        {
            overlaps = GetColliderCache();
            return 0;
        }
    }
}
