using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_Cast : ScenePreview<RaycastHit>
    {
        protected override Rect GetButtonPositionForElement(ElementIndex<RaycastHit> element)
        {
            return Rect.zero;
        }
        protected override Result<RaycastHit> GetResult(PhysicsQuery query)
        {
            return new(null, 0);
        }
        protected override string GetTooltipForElement(RaycastHit element)
        {
            return string.Empty;
        }
        protected override bool IsElementValid(RaycastHit element)
        {
            return false;
        }
    }
}