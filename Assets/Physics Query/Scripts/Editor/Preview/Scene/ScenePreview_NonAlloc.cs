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
                TElement element = result[index];
                string label = GetLabel(element, index);
                DrawButton(element, label);
            }
            else
            {
                SceneButtonStrategy.DrawEmptyButton();
            }
        }
        private void DrawButton(TElement element, string label)
        {
            if (DrawButton((object)element, label))
            {
                ClickCollider(GetCollider(element));
            }
        }

        protected abstract Result<TElement> GetResult(PhysicsQuery query);
        protected abstract string GetLabel(TElement element, int index);
        protected abstract Collider GetCollider(TElement element);
    }
}