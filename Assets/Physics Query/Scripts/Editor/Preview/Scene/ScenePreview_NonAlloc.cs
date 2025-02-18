using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class ScenePreview_NonAlloc<TElement> : ScenePreview
    {
        private readonly SceneButtonStrategy<TElement> _button;

        protected ScenePreview_NonAlloc()
        {
            _button = GetSceneButtonStrategy();
        }

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
            if (_button.Draw(element, label))
            {
                ClickCollider(GetCollider(element));
            }
        }

        protected abstract Result<TElement> GetResult(PhysicsQuery query);
        protected abstract string GetLabel(TElement element, int index);
        protected abstract Component GetCollider(TElement element);
        protected abstract SceneButtonStrategy<TElement> GetSceneButtonStrategy();
    }
}