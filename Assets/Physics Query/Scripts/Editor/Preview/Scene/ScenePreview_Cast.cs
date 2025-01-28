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
                DrawButton(hit);
            }
            else
            {
                DrawEmptyButton();
            }
            Handles.EndGUI();
        }
        private void DrawButton(RaycastHit hit)
        {
            if (IsAbleToDisplay(hit))
            {
                Rect position = GetButtonPosition(hit, GetContent(hit));
                DrawButton(position, hit);
            }
            else
            {
                DrawEmptyButton();
            }
        }
        private void DrawButton(Rect position, RaycastHit hit) 
        {
            if (GUI.Button(position, GetContent(hit), ButtonStyle))
            {
                NotifyElementClicked(hit);
            }
        }
        private GUIContent GetContent(RaycastHit hit)
        {
            return new("Hit", GetTooltip(hit));
        }
    }
}