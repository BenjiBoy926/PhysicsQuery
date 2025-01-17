using UnityEngine;

namespace PhysicsQuery
{
    public abstract class ResultDisplay
    {
        public abstract void DrawInspectorGUI(GizmoMode preview);
    }
    public abstract class ResultDisplay<TElement> : ResultDisplay
    {
        public override void DrawInspectorGUI(GizmoMode preview)
        {
            GUI.enabled = false;
            Result<TElement> result = GetResult(preview);
            for (int i = 0; i < result.Count; i++)
            {
                DrawElementInspectorGUI(result.Get(i), i);
            }
            GUI.enabled = true;
        }

        protected abstract Result<TElement> GetResult(GizmoMode preview);
        protected abstract void DrawElementInspectorGUI(TElement element, int index);
    }
}