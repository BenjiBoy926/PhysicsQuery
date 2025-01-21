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
        if (_target.transform.position.x > -1)
        {
            return;
        }
        Handles.BeginGUI();
        GUIContent content = new("You're too far to the left!");
        GUIStyle style = GUI.skin.box;
        Rect rect = HandleUtility.WorldPointToSizedRect(_target.transform.position, content, style);
        GUI.Box(rect, content, style);
        Handles.EndGUI();
    }
}
