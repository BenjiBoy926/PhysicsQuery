using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_Check : InspectorPreview<Collider>
    {
        public override void HighlightElement(object element)
        {

        }
        protected override void DrawElementInspectorGUI(Collider element, int index)
        {

        }
        protected override Result<Collider> GetResult(PhysicsQuery query)
        {
            return new(null, 0);
        }
    }
}