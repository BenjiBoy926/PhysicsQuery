using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_Cast : InspectorPreview<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(PhysicsQuery query)
        {
            return new(null, 0);
        }
        protected override void DrawElementInspectorGUI(RaycastHit element, int index)
        {
            
        }
        public override void HighlightElement(object element)
        {
            
        }
    }
}