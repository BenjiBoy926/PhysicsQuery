using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class Preview_Cast : Preview
    {
        public override string Label => "Cast";

        public Preview_Cast(PhysicsQuery query) : base(query) { }

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