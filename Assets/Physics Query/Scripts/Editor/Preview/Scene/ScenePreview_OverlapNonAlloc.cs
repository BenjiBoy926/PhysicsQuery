using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_OverlapNonAlloc : ScenePreview<Collider>
    {
        protected override Result<Collider> GetResult(PhysicsQuery query)
        {
            return query.OverlapNonAlloc();
        }
        protected override Rect GetButtonPositionForElement(ElementIndex<Collider> element)
        {
            Vector2 size = GetButtonSize(element);
            Vector2 topLeftGUIPosition = HandleUtility.WorldToGUIPoint(element.Value.transform.position);
            Vector2 position = topLeftGUIPosition - 0.5f * size;
            return new(position, size);
        }
        protected override string GetTooltipForElement(Collider element)
        {
            return element.name;
        }
        protected override bool IsElementValid(Collider element)
        {
            return element != null && IsPositionOnScreen(element.transform.position);
        }
    }
}