using UnityEngine;
using UnityEditor;

namespace PQuery.Editor
{
    public class SceneButtonStrategy_RaycastHit : SceneButtonStrategy<AgnosticRaycastHit>
    {
        protected override Rect GetPosition(AgnosticRaycastHit value, string label)
        {
            Vector3 center = value.Point;
            Vector3 offset = 2 * Preferences.HitSphereRadius.Value * Vector3.up;
            Vector3 bottomEdgeWorldPosition = center + offset;

            Vector2 size = GetSize(value, label);
            Vector2 bottomLeftGUIPosition = HandleUtility.WorldToGUIPoint(bottomEdgeWorldPosition);
            Vector2 topLeftGUIPosition = bottomLeftGUIPosition + Vector2.down * size.y;
            Vector2 position = topLeftGUIPosition + 0.5f * size.x * Vector2.left;

            return new(position, size);
        }
        protected override string GetTooltip(AgnosticRaycastHit value)
        {
            return $"Point: {value.Point}\n" +
                $"Normal: {value.Normal}\n" +
                $"Distance: {value.Distance}";
        }
        protected override bool IsVisible(AgnosticRaycastHit value)
        {
            return value.Collider != null && IsPositionOnScreen(value.Point);
        }
    }
}