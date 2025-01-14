
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class PhysicsQueryPreview_CastNonAlloc : PhysicsQueryPreview
    {
        public override string Label => "Cast Non Alloc";
        
        public PhysicsQueryPreview_CastNonAlloc(PhysicsQuery query) : base(query)
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