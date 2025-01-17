using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class ResultDisplay_Overlap : ResultDisplay<Collider>
    {
        protected override void DrawElementInspectorGUI(Collider element, int index)
        {
            string label = $"Element {index}";
            EditorGUILayout.ObjectField(label, element, element.GetType(), true);
        }
        protected override Result<Collider> GetResult(Preview preview)
        {
            return preview.OverlapResult;
        }
    }
}