using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_Cast : ScenePreview
    {
        public override void DrawSceneGUI(PhysicsQuery query)
        {
            bool didHit = query.Cast(out RaycastHit hit);
            Handles.BeginGUI();
            if (didHit)
            {
                DrawButton(hit, "Hit");
            }
            else
            {
                SceneButtonStrategy.DrawEmptyButton();
            }
            Handles.EndGUI();
        }
        protected override SceneButtonStrategy GetButtonStrategy()
        {
            return new SceneButtonStrategy_RaycastHit();
        }
    }
}