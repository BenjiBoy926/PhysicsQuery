using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_OverlapNonAlloc : InspectorPreview_NonAlloc<Collider>
    {
        protected override void DrawElementInspectorGUI(PhysicsQuery3D query, Collider element, int index)
        {
            string label = $"Element {index}";
            EditorGUILayout.ObjectField(label, element, element.GetType(), true);
        }
        protected override Result<Collider> GetResult(PhysicsQuery3D query)
        {
            return query.OverlapNonAlloc();
        }
    }
}