using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class Preview_Cast : Preview
    {
        public override string Label => "Cast";

        public Preview_Cast(PreviewForm form) : base(form)
        {
        }

        public override void Draw()
        {
            Form.DrawCast();
        }
    }
}