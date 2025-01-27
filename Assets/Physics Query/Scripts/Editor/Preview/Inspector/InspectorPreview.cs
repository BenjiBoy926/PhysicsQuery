using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class InspectorPreview
    {
        public abstract void DrawInspectorGUI(PhysicsQuery query);
        public abstract void HighlightElement(object element);
    }
    public abstract class InspectorPreview<TElement> : InspectorPreview
    {
        private static readonly string CacheFullMessage = "Cache capacity reached. " +
            "Reduce the number of colliders in the scene " +
            "or increase the query's cache capacity " +
            "to ensure correct results";

        public override void DrawInspectorGUI(PhysicsQuery query)
        {
            Result<TElement> result = GetResult(query);
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

        protected abstract Result<TElement> GetResult(PhysicsQuery query);
        protected abstract void DrawElementInspectorGUI(TElement element, int index);
    }
}