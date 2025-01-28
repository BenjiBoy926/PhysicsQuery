using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_Check : ScenePreview<Collider>
    {
        protected override Rect GetButtonPositionForElement(ElementIndex<Collider> element)
        {
            return Rect.zero;
        }
        protected override Result<Collider> GetResult(PhysicsQuery query)
        {
            return new(null, 0);
        }
        protected override string GetTooltipForElement(Collider element)
        {
            return string.Empty;
        }
        protected override bool IsElementValid(Collider element)
        {
            return false;
        }
    }
}