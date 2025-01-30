using System;
using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class SceneButtonStrategy
    {
        protected static Vector2 AdditionalButtonSize => _additionalButtonSize;
        protected static GUIStyle ButtonStyle => _buttonStyle ??= new(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleCenter
        };

        private static readonly Vector2 _additionalButtonSize = new(30, 10);
        private static GUIStyle _buttonStyle;

        public static void DrawEmptyButton()
        {
            GUI.Button(Rect.zero, GUIContent.none, GUIStyle.none);
        }
    }
    public abstract class SceneButtonStrategy<TValue> : SceneButtonStrategy
    {
        public bool Draw(TValue value, string label)
        {
            if (IsVisible(value))
            {
                Rect position = GetPosition(value, label);
                GUIContent content = GetContent(value, label);
                return GUI.Button(position, content, ButtonStyle);
            }
            else
            {
                DrawEmptyButton();
                return false;
            }
        }
        protected Vector2 GetSize(TValue value, string label)
        {
            GUIContent content = GetContent(value, label);
            return ButtonStyle.CalcSize(content) + AdditionalButtonSize;
        }
        protected GUIContent GetContent(TValue value, string label)
        {
            return new(label, GetTooltip(value));
        }
        protected bool IsPositionOnScreen(Vector3 worldPosition)
        {
            SceneView scene = SceneView.currentDrawingSceneView;
            if (scene == null)
            {
                return false;
            }
            Camera camera = scene.camera;
            if (camera == null)
            {
                return false;
            }
            Vector3 viewportPosition = camera.WorldToViewportPoint(worldPosition);
            return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1 && viewportPosition.z > 0;
        }

        protected abstract Rect GetPosition(TValue value, string label);
        protected abstract string GetTooltip(TValue value);
        protected abstract bool IsVisible(TValue value);
    }
}