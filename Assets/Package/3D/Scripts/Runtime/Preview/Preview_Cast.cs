using UnityEngine;

namespace PhysicsQuery
{
    public class Preview_Cast : Preview
    {
        public override string Label => "Cast";

        public Preview_Cast(PreviewForm form) : base(form)
        {
        }

        public override void DrawGizmos()
        {
            Form.DrawCastGizmos();
        }
    }
}