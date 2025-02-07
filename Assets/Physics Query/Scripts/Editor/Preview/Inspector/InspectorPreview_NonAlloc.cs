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

        public override void DrawInspectorGUI(PhysicsQuery3D query)
        {
            Result<TElement> result = GetResult(query);
            DrawEachElementInspectorGUI(query, result);
            if (result.IsFull)
            {
                EditorGUILayout.HelpBox(CacheFullMessage, MessageType.Error);
            }
        }

        private void DrawEachElementInspectorGUI(PhysicsQuery3D query, Result<TElement> result)
        {
            GUI.enabled = false;
            for (int i = 0; i < result.Count; i++)
            {
                DrawElementInspectorGUI(query, result[i], i);
            }
            GUI.enabled = true;
        }

        protected abstract Result<TElement> GetResult(PhysicsQuery3D query);
        protected abstract void DrawElementInspectorGUI(PhysicsQuery3D query, TElement element, int index);
    }
}