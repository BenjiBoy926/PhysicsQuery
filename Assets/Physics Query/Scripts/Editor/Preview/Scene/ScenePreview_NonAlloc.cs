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
                DrawButton(result[index], index.ToString());
            }
            else
            {
                SceneButtonStrategy.DrawEmptyButton();
            }
        }

        protected abstract Result<TElement> GetResult(PhysicsQuery query);
    }
}