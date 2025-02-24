using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class InspectorPreview_NonAlloc<TElement> : InspectorPreview
    {
        private static readonly string CacheFullMessage = "Cache capacity reached. " +
            "Reduce the number of colliders in the scene " +
            "or increase the query's cache capacity " +
            "to ensure correct results";

        public override void DrawInspectorGUI(PhysicsQuery query)
        {
            Result<TElement> result = GetResult(query);
            DrawEachElementInspectorGUI(query, result);
            if (result.IsFull)
            {
                EditorGUILayout.HelpBox(CacheFullMessage, MessageType.Error);
            }
        }

        private void DrawEachElementInspectorGUI(PhysicsQuery query, Result<TElement> result)
        {
            GUI.enabled = false;
            for (int i = 0; i < result.Count; i++)
            {
                DrawElementInspectorGUI(query, result[i], i);
            }
            GUI.enabled = true;
        }

        protected abstract Result<TElement> GetResult(PhysicsQuery query);
        protected abstract void DrawElementInspectorGUI(PhysicsQuery query, TElement element, int index);
    }
}