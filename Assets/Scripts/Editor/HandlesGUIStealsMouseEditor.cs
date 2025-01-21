using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HandlesGUIStealsMouse))]
public class HandlesGUIStealsMouseEditor : Editor
{
    private HandlesGUIStealsMouse _target;

    private void OnEnable()
    {
        _target = target as HandlesGUIStealsMouse;
    }
    private void OnSceneGUI()
    {
        GUIContent content = new("You're too far to the left!");
        GUIStyle style = GUI.skin.button;
        Rect rect = HandleUtility.WorldPointToSizedRect(_target.transform.position, content, style);
        if (_target.transform.position.x > -1)
        {
            content = GUIContent.none;
            style = GUIStyle.none;
            rect = Rect.zero;
        }
        Handles.BeginGUI();
        GUI.Button(rect, content, style);
        Handles.EndGUI();
    }
}
