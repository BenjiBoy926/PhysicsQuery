using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class ScenePreview_NonAlloc<TElement> : ScenePreview
    {
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
            if (IsAbleToDisplayElement(element.Value))
            {
                Rect position = GetButtonPosition(element);
                DrawButton(position, element);
            }
            else
            {
                DrawEmptyButton();
            }
        }
        private void DrawButton(Rect position, ElementIndex<TElement> element)
        {
            if (GUI.Button(position, GetButtonContent(element), ButtonStyle))
            {
                NotifyElementClicked(element.Value);
            }
        }
        protected GUIContent GetButtonContent(ElementIndex<TElement> element)
        {
            return new(element.Index.ToString(), GetTooltipForElement(element.Value));
        }

        protected abstract Result<TElement> GetResult(PhysicsQuery query);
        protected abstract Rect GetButtonPosition(ElementIndex<TElement> element);
        protected abstract string GetTooltipForElement(TElement element);
        protected abstract bool IsAbleToDisplayElement(TElement element);
    }
}