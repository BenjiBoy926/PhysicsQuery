using UnityEngine;
using UnityEditor;

namespace PQuery.Editor
{
    public class SceneButtonStrategy_RaycastHit : SceneButtonStrategy<RaycastHit>
    {
        protected override Rect GetPosition(RaycastHit value, string label)
        {
            Vector3 center = value.point;
            Vector3 offset = 2 * Preferences.HitSphereRadius.Value * Vector3.up;
            Vector3 bottomEdgeWorldPosition = center + offset;

            Vector2 size = GetSize(value, label);
            Vector2 bottomLeftGUIPosition = HandleUtility.WorldToGUIPoint(bottomEdgeWorldPosition);
            Vector2 topLeftGUIPosition = bottomLeftGUIPosition + Vector2.down * size.y;
            Vector2 position = topLeftGUIPosition + 0.5f * size.x * Vector2.left;

            return new(position, size);
        }
        protected override string GetTooltip(RaycastHit value)
        {
            return $"Point: {value.point}\n" +
                $"Normal: {value.normal}\n" +
                $"Distance: {value.distance}";
        }
        protected override bool IsVisible(RaycastHit value)
        {
            return value.collider != null && IsPositionOnScreen(value.point);
        }
    }
}