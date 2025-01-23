using UnityEditor;
using UnityEngine;

namespace PhysicsQuery
{
    public abstract class InspectorPreview
    {
        public abstract void DrawInspectorGUI(PreviewResults results);
        public abstract void HighlightElement(PreviewResults results, int index);
    }
    public abstract class InspectorPreview<TElement> : InspectorPreview
    {
        private static readonly string CacheFullMessage = "Cache capacity reached. " +
            "Reduce the number of colliders in the scene " +
            "or increase the query's cache capacity " +
            "to ensure correct results";

        public override void DrawInspectorGUI(PreviewResults results)
        {
            Result<TElement> result = GetResult(results);
            DrawEachElementInspectorGUI(result);
            if (result.IsFull)
            {
                EditorGUILayout.HelpBox(CacheFullMessage, MessageType.Error);
            }
        }

        private void DrawEachElementInspectorGUI(Result<TElement> result)
        {
            GUI.enabled = false;
            for (int i = 0; i < result.Count; i++)
            {
                DrawElementInspectorGUI(result[i], i);
            }
            GUI.enabled = true;
        }

        protected abstract Result<TElement> GetResult(PreviewResults results);
        protected abstract void DrawElementInspectorGUI(TElement element, int index);
    }
}