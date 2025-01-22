using UnityEditor;
using UnityEngine;

namespace PhysicsQuery
{
    public abstract class InspectorPreview
    {
        public abstract void DrawInspectorGUI(GizmoPreview gizmos);
        public abstract void HighlightElement(GizmoPreview gizmos, int index);
    }
    public abstract class InspectorPreview<TElement> : InspectorPreview
    {
        private static readonly string CacheFullMessage = "Cache capacity reached. " +
            "Reduce the number of colliders in the scene, " +
            "reduce the query's size, " +
            "or increase the query's cache capacity " +
            "to ensure correct results";

        public override void DrawInspectorGUI(GizmoPreview gizmos)
        {
            Result<TElement> result = GetResult(gizmos);
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

        protected abstract Result<TElement> GetResult(GizmoPreview gizmos);
        protected abstract void DrawElementInspectorGUI(TElement element, int index);
    }
}