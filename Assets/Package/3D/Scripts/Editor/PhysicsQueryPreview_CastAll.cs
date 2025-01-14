using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class PhysicsQueryPreview_CastAll : PhysicsQueryPreview
    {
        public override string Label => "Cast All";

        public PhysicsQueryPreview_CastAll(PhysicsQuery query) : base(query)
        {
        }   

        public override void Draw()
        {
            RaycastHit[] hits = Query.CastAll();
            if (hits.Length > 0)
            {
                DrawHits(hits);
                DrawNoHit(hits[^1].point);
            }
            else
            {
                DrawNoHit(WorldRay.origin);
            }
        }
    }
}