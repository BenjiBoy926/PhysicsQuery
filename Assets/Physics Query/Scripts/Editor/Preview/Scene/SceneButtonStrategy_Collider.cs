using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class SceneButtonStrategy_Collider : SceneButtonStrategy<Component>
    {
        protected override Rect GetPosition(Component value, string label)
        {
            Vector2 size = GetSize(value, label);
            Vector2 topLeftGUIPosition = HandleUtility.WorldToGUIPoint(value.transform.position);
            Vector2 position = topLeftGUIPosition - 0.5f * size;
            return new(position, size);
        }
        protected override string GetTooltip(Component value)
        {
            return value.name;
        }
        protected override bool IsVisible(Component value)
        {
            return value != null && IsPositionOnScreen(value.transform.position);
        }
    }
}