using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_OverlapNonAlloc : InspectorPreview_NonAlloc<Component>
    {
        protected override void DrawElementInspectorGUI(PhysicsQuery query, Component element, int index)
        {
            string label = $"Element {index}";
            EditorGUILayout.ObjectField(label, element, element.GetType(), true);
        }
        protected override Result<Component> GetResult(PhysicsQuery query)
        {
            return query.AgnosticOverlapNonAlloc();
        }
    }
}