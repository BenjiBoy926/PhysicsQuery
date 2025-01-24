using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class InspectorPreview_Overlap : InspectorPreview<Collider>
    {
        protected override void DrawElementInspectorGUI(Collider element, int index)
        {
            string label = $"Element {index}";
            EditorGUILayout.ObjectField(label, element, element.GetType(), true);
        }
        protected override Result<Collider> GetResult(PreviewResults results)
        {
            return results.OverlapResult;
        }
        public override void HighlightElement(PreviewResults results, int index)
        {
            EditorGUIUtility.PingObject(results.OverlapResult[index]);
        }
    }
}