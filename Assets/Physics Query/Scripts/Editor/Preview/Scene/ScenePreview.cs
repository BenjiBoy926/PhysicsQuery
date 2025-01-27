using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class ScenePreview
    {
        public delegate void ElementClickedHandler(object element);
        public event ElementClickedHandler ElementClicked = delegate { };
        protected void NotifyElementClicked(object element)
        {
            ElementClicked(element);
        }

        public abstract void DrawSceneGUI(PhysicsQuery query);
    }
    public abstract class ScenePreview<TElement> : ScenePreview
    {
        private GUIStyle ButtonStyle => _buttonStyle ??= new(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleCenter
        };

        private static readonly Vector2 _additionalButtonSize = new(30, 10);
        private GUIStyle _buttonStyle;

        public override void DrawSceneGUI(PhysicsQuery query)
        {
            Result<TElement> result = GetResult(query);
            Handles.BeginGUI();
            for (int i = 0; i < result.Capacity; i++)
            {
                DrawButton(result, i);
            }
            Handles.EndGUI();
        }

        private void DrawButton(Result<TElement> result, int index)
        {
            if (result.IsIndexValid(index))
            {
                DrawButton(new(result, index));
            }
            else
            {
                DrawEmptyButton();
            }
        }

        private void DrawButton(ElementIndex<TElement> element)
        {
            if (IsElementValid(element.Value))
            {
                Rect position = GetButtonPositionForElement(element);
                DrawButton(position, element);
            }
            else
            {
                DrawEmptyButton();
            }
        }
        private void DrawButton(Rect position, ElementIndex<TElement> element)
        {
            if (DrawButton(position, GetContentForElement(element), ButtonStyle))
            {
                NotifyElementClicked(element.Value);
            }
        }
        private void DrawEmptyButton()
        {
            DrawButton(Rect.zero, GUIContent.none, GUIStyle.none);
        }
        private bool DrawButton(Rect position, GUIContent content, GUIStyle style)
        {
            return GUI.Button(position, content, style);
        }
        protected Vector2 GetButtonSize(ElementIndex<TElement> element)
        {
            GUIContent content = GetContentForElement(element);
            return ButtonStyle.CalcSize(content) + _additionalButtonSize;
        }
        protected GUIContent GetContentForElement(ElementIndex<TElement> element)
        {
            return new(element.Index.ToString(), GetTooltipForElement(element.Value));
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

        protected abstract Result<TElement> GetResult(PhysicsQuery query);
        protected abstract Rect GetButtonPositionForElement(ElementIndex<TElement> element);
        protected abstract string GetTooltipForElement(TElement element);
        protected abstract bool IsElementValid(TElement element);
    }
}