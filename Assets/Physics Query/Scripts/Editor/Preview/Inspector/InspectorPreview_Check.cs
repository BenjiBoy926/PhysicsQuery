using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_Check : InspectorPreview
    {
        public override void DrawInspectorGUI(PhysicsQuery query)
        {
            // Nothing in the inspector to draw - check does not return a collider
        }
    }
}