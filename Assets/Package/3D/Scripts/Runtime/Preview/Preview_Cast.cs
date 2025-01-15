using UnityEngine;

namespace PhysicsQuery
{
    public class Preview_Cast : Preview
    {
        public override string Label => "Cast";

        public Preview_Cast(PreviewShape form) : base(form)
        {
        }

        public override void DrawGizmos()
        {
            Shape.DrawCastGizmos();
        }
    }
}