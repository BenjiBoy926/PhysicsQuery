using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_Cast : InspectorPreview
    {
        public override void DrawInspectorGUI(PhysicsQuery query)
        {
            bool result = query.BoxedCast(out BoxedRaycastHit hit);
            if (result)
            {
                GUI.enabled = false;
                DrawEachPropertyInspectorGUI(hit);
                GUI.enabled = true;
            }
        }
    }
}