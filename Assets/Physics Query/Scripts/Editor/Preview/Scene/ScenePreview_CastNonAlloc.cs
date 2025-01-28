using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_CastNonAlloc : ScenePreview_NonAlloc<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(PhysicsQuery query)
        {
            return query.CastNonAlloc(ResultSort.Distance);
        }
        protected override Rect GetButtonPosition(ElementIndex<RaycastHit> element)
        {
            return GetButtonPosition(element.Value, GetButtonContent(element));
        }
        protected override string GetTooltipForElement(RaycastHit element)
        {
            return GetTooltip(element);
        }
        protected override bool IsAbleToDisplayElement(RaycastHit element)
        {
            return IsAbleToDisplay(element);
        }
    }
}