using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class ScenePreview
    {
        public delegate void ElementClickedHandler(object element);
        public event ElementClickedHandler ElementClicked = delegate { };

        protected GUIStyle ButtonStyle => _buttonStyle ??= new(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleCenter
        };

        private static readonly Vector2 _additionalButtonSize = new(30, 10);
        private GUIStyle _buttonStyle;

        protected void NotifyElementClicked(object element)
        {
            ElementClicked(element);
        }
        protected void DrawEmptyButton()
        {
            GUI.Button(Rect.zero, GUIContent.none, GUIStyle.none);
        }
        protected Vector2 GetButtonSize(GUIContent content)
        {
            return ButtonStyle.CalcSize(content) + _additionalButtonSize;
        }
        protected Rect GetButtonPosition(RaycastHit hit, GUIContent content)
        {
            Vector3 center = hit.point;
            Vector3 offset = 2 * Preferences.HitSphereRadius.Value * Vector3.up;
            Vector3 bottomEdgeWorldPosition = center + offset;

            Vector2 size = GetButtonSize(content);
            Vector2 bottomLeftGUIPosition = HandleUtility.WorldToGUIPoint(bottomEdgeWorldPosition);
            Vector2 topLeftGUIPosition = bottomLeftGUIPosition + Vector2.down * size.y;
            Vector2 position = topLeftGUIPosition + 0.5f * size.x * Vector2.left;

            return new(position, size);
        }
        protected string GetTooltip(RaycastHit element)
        {
            return $"Collider: {element.collider.name}\n" +
                $"Point: {element.point}\n" +
                $"Normal: {element.normal}\n" +
                $"Distance: {element.distance}";
        }
        protected bool IsAbleToDisplay(RaycastHit element)
        {
            return element.collider != null && IsPositionOnScreen(element.point);
        }
        protected bool IsPositionOnScreen(Vector3 worldPosition)
        {
            SceneView scene = SceneView.currentDrawingSceneView;
            if (scene == null)
            {
                return false;
            }
            Camera camera = scene.camera;
            if (camera == null)
            {
                return false;
            }
            Vector3 viewportPosition = camera.WorldToViewportPoint(worldPosition);
            return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1 && viewportPosition.z > 0;
        }

        public abstract void DrawSceneGUI(PhysicsQuery query);
    }
}