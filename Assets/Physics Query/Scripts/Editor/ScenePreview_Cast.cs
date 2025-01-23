using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class ScenePreview_Cast : ScenePreview<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(PreviewResults results)
        {
            return results.CastResult;
        }
        protected override Rect GetButtonPositionForElement(ElementIndex<RaycastHit> element)
        {
            Vector3 center = element.Value.point;
            Vector3 offset = 2 * Preferences.HitSphereRadius * Vector3.up;
            Vector3 bottomEdgeWorldPosition = center + offset;

            Vector2 size = GetButtonSize(element);
            Vector2 bottomLeftGUIPosition = HandleUtility.WorldToGUIPoint(bottomEdgeWorldPosition);
            Vector2 topLeftGUIPosition = bottomLeftGUIPosition + Vector2.down * size.y;
            Vector2 position = topLeftGUIPosition + 0.5f * size.x * Vector2.left;

            return new(position, size);
        }
        protected override string GetTooltipForElement(RaycastHit element)
        {
            return $"Collider: {element.collider.name}\n" +
                $"Point: {element.point}\n" +
                $"Normal: {element.normal}\n" +
                $"Distance: {element.distance}";
        }
        protected override bool IsElementValid(RaycastHit element)
        {
            return element.collider != null && IsPositionOnScreen(element.point);
        }
    }
}