using UnityEngine;

namespace PhysicsQuery
{
    public abstract class InspectorPreview
    {
        public abstract void DrawInspectorGUI(GizmoPreview gizmos);
    }
    public abstract class InspectorPreview<TElement> : InspectorPreview
    {
        public override void DrawInspectorGUI(GizmoPreview gizmos)
        {
            GUI.enabled = false;
            Result<TElement> result = GetResult(gizmos);
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