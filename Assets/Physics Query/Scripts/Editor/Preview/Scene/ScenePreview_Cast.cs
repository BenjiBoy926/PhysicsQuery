using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_Cast : ScenePreview
    {
        private readonly SceneButtonStrategy_RaycastHit _button = new();

        public override void DrawSceneGUI(PhysicsQuery query)
        {
            bool didHit = query.AgnosticCast(out AgnosticRaycastHit hit);
            Handles.BeginGUI();
            if (didHit)
            {
                DrawButton(hit);
            }
            else
            {
                SceneButtonStrategy.DrawEmptyButton();
            }
            Handles.EndGUI();
        }
        private void DrawButton(AgnosticRaycastHit hit)
        {
            if (_button.Draw(hit, hit.Collider.name))
            {
                ClickCollider(hit.Collider);
            }
        }
    }
}