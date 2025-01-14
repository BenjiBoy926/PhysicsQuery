using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class PhysicsQueryPreview_Cast : PhysicsQueryPreview
    {
        public override string Label => "Cast";

        public PhysicsQueryPreview_Cast(PhysicsQuery query) : base(query) { }

        public override void Draw()
        {
            bool didHit = Query.Cast(out RaycastHit hit);
            if (didHit)
            {
                DrawHit(hit);
            }
            else
            {
                DrawNoHit(WorldRay.origin);
            }
        }
    }
}