using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class ScenePreview
    {
        public abstract void DrawSceneGUI(GizmoPreview gizmo);
    }
    public abstract class ScenePreview<TElement> : ScenePreview
    {
        private GUIStyle Style => _style ??= new(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleCenter
        };
        private GUIStyle _style;
        private static readonly Vector2 _additionalSpace = new(30, 0);

        public override void DrawSceneGUI(GizmoPreview gizmo)
        {
            Result<TElement> result = GetResult(gizmo);
            Handles.BeginGUI();
            for (int i = 0; i < result.Capacity; i++)
            {
                DrawButton(result, i);
            }
            Handles.EndGUI();
        }

        private void DrawButton(Result<TElement> result, int index)
        {
            if (result.IsIndexValid(index))
            {
                Rect position = GetButtonPositionForElement(result[index], index);
                DrawNonEmptyButton(position, result[index], index);
            }
            else
            {
                DrawEmptyButton();
            }
        }
        protected void DrawNonEmptyButton(Rect position, TElement element, int index)
        {
            DrawButton(position, GetContentForElement(element, index), Style);
        }
        protected void DrawEmptyButton()
        {
            DrawButton(Rect.zero, GUIContent.none, GUIStyle.none);
        }
        private void DrawButton(Rect position, GUIContent content, GUIStyle style)
        {
            GUI.Button(position, content, style);
        }
        protected Vector2 GetButtonSize(TElement element, int index)
        {
            GUIContent content = GetContentForElement(element, index);
            return Style.CalcSize(content) + _additionalSpace;
        }
        protected GUIContent GetContentForElement(TElement element, int index)
        {
            return new(index.ToString(), GetTooltipForElement(element));
        }

        protected abstract Result<TElement> GetResult(GizmoPreview gizmo);
        protected abstract Rect GetButtonPositionForElement(TElement element, int index);
        protected abstract string GetTooltipForElement(TElement element);
    }

    public class ScenePreview_Cast : ScenePreview<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(GizmoPreview gizmo)
        {
            return gizmo.CastResult;
        }
        protected override Rect GetButtonPositionForElement(RaycastHit element, int index)
        {
            Vector3 center = element.point;
            Vector3 offset = 2 * GizmoShape.HitSphereRadius * Vector3.up;
            Vector3 bottomEdgeWorldPosition = center + offset;

            Vector2 size = GetButtonSize(element, index);
            Vector2 bottomLeftGUIPosition = HandleUtility.WorldToGUIPoint(bottomEdgeWorldPosition);
            Vector2 topLeftGUIPosition = bottomLeftGUIPosition + Vector2.down * size.y;
            Vector2 position = topLeftGUIPosition + 0.5f * size.x * Vector2.left;

            return new(position, size);
        }
        protected override string GetTooltipForElement(RaycastHit element)
        {
            return element.ToString();
        }
    }
    public class ScenePreview_Overlap : ScenePreview<Collider>
    {
        protected override Result<Collider> GetResult(GizmoPreview gizmo)
        {
            return gizmo.OverlapResult;
        }
        protected override Rect GetButtonPositionForElement(Collider element, int index)
        {
            Vector2 size = GetButtonSize(element, index);
            Vector2 topLeftGUIPosition = HandleUtility.WorldToGUIPoint(element.transform.position);
            Vector2 position = topLeftGUIPosition - 0.5f * size;
            return new(position, size);
        }
        protected override string GetTooltipForElement(Collider element)
        {
            return element.name;
        }
    }
}