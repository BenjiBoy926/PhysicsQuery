using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_Check : InspectorPreview
    {
        public override void DrawInspectorGUI(PhysicsQuery query)
        {
            // Nothing in the inspector to draw - check does not return a collider
        }
        public override void OnColliderClicked(Collider collider)
        {
            // Can't highlight anything - check does not return a collider
        }
    }
}