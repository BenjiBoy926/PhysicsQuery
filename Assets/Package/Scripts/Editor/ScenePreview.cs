using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class ScenePreview
    {
        public delegate void ElementClickedHandler(int index);
        public event ElementClickedHandler ElementClicked = delegate { };
        protected void NotifyElementClicked(int index)
        {
            ElementClicked(index);
        }

        public abstract void DrawSceneGUI(GizmoPreview gizmo);
    }
    public abstract class ScenePreview<TElement> : ScenePreview
    {
        private GUIStyle Style => _style ??= new(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleCenter
        };
        private GUIStyle _style;
        private static readonly Vector2 _additionalSpace = new(30, 0);

        public override void DrawSceneGUI(GizmoPreview gizmo)
        {
            Result<TElement> result = GetResult(gizmo);
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
                DrawButtonForElement(new(result, index));
            }
            else
            {
                DrawEmptyButton();
            }
        }
        private void DrawButtonForElement(ElementInCollection<TElement> element)
        {
            if (IsElementValid(element.Value))
            {
                Rect position = GetButtonPositionForElement(element);
                DrawNonEmptyButton(position, element);
            }
        }
        private void DrawNonEmptyButton(Rect position, ElementInCollection<TElement> element)
        {
            if (DrawButton(position, GetContentForElement(element), Style))
            {
                NotifyElementClicked(element.Index);
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
        protected Vector2 GetButtonSize(ElementInCollection<TElement> element)
        {
            GUIContent content = GetContentForElement(element);
            return Style.CalcSize(content) + _additionalSpace;
        }
        protected GUIContent GetContentForElement(ElementInCollection<TElement> element)
        {
            return new(element.Index.ToString(), GetTooltipForElement(element.Value));
        }

        protected abstract Result<TElement> GetResult(GizmoPreview gizmo);
        protected abstract Rect GetButtonPositionForElement(ElementInCollection<TElement> element);
        protected abstract string GetTooltipForElement(TElement element);
        protected abstract bool IsElementValid(TElement element);
    }
}