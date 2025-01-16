using UnityEngine;

namespace PhysicsQuery
{
    public class Preview_Cast : IPreview
    {
        public string Label => "Cast";
        public void DrawGizmos(PreviewShape shape)
        {
            shape.DrawCastGizmos();
        }
    }
}