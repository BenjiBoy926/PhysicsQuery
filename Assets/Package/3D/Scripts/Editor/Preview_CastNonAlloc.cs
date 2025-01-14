
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class Preview_CastNonAlloc : Preview
    {
        public override string Label => "Cast Non Alloc";
        
        public Preview_CastNonAlloc(PhysicsQuery query) : base(query)
        {
        }

        public override void Draw()
        {
            int hitCount = Query.CastNonAlloc(out RaycastHit[] hits);
            if (hitCount > 0)
            {
                DrawHits(hits, hitCount);
                DrawNoHit(hits[hitCount - 1].point);
            }
            else
            {
                DrawNoHit(WorldRay.origin);
            }
        }
    }
}