using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_Cast : ScenePreview
    {
        private readonly SceneButtonStrategy_RaycastHit _button = new();

        public override void DrawSceneGUI(PhysicsQuery3D query)
        {
            bool didHit = query.Cast(out RaycastHit hit);
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
        private void DrawButton(RaycastHit hit)
        {
            if (_button.Draw(hit, hit.collider.name))
            {
                ClickCollider(hit.collider);
            }
        }
    }
}