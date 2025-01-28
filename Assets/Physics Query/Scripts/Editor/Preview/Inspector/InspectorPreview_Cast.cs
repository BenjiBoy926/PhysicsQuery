using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_Cast : InspectorPreview
    {
        public override void DrawInspectorGUI(PhysicsQuery query)
        {
            bool result = query.Cast(out RaycastHit hit);
            if (result)
            {
                GUI.enabled = false;
                DrawEachPropertyInspectorGUI(hit);
                GUI.enabled = true;
            }
        }
        public override void HighlightElement(object element)
        {
            RaycastHit hit = CastHighlightElement<RaycastHit>(element);
            EditorGUIUtility.PingObject(hit.collider);
        }
    }
}