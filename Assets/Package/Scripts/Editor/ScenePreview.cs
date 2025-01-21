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
                ElementInCollection<TElement> element = new(result[index], index);
                Rect position = GetButtonPositionForElement(element);
                DrawNonEmptyButton(position, element);
            }
            else
            {
                DrawEmptyButton();
            }
        }
        protected void DrawNonEmptyButton(Rect position, ElementInCollection<TElement> element)
        {
            DrawButton(position, GetContentForElement(element), Style);
        }
        protected void DrawEmptyButton()
        {
            DrawButton(Rect.zero, GUIContent.none, GUIStyle.none);
        }
        private void DrawButton(Rect position, GUIContent content, GUIStyle style)
        {
            GUI.Button(position, content, style);
        }
        protected Vector2 GetButtonSize(ElementInCollection<TElement> element)
        {
            GUIContent content = GetContentForElement(element);
            return Style.CalcSize(content) + _additionalSpace;
        }
        protected GUIContent GetContentForElement(ElementInCollection<TElement> element)
        {
            return new(element.Index.ToString(), GetTooltipForElement(element.Value));
        }

        protected abstract Result<TElement> GetResult(GizmoPreview gizmo);
        protected abstract Rect GetButtonPositionForElement(ElementInCollection<TElement> element);
        protected abstract string GetTooltipForElement(TElement element);
    }

    public class ScenePreview_Cast : ScenePreview<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(GizmoPreview gizmo)
        {
            return gizmo.CastResult;
        }
        protected override Rect GetButtonPositionForElement(ElementInCollection<RaycastHit> element)
        {
            Vector3 center = element.Value.point;
            Vector3 offset = 2 * GizmoShape.HitSphereRadius * Vector3.up;
            Vector3 bottomEdgeWorldPosition = center + offset;

            Vector2 size = GetButtonSize(element);
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
        protected override Rect GetButtonPositionForElement(ElementInCollection<Collider> element)
        {
            Vector2 size = GetButtonSize(element);
            Vector2 topLeftGUIPosition = HandleUtility.WorldToGUIPoint(element.Value.transform.position);
            Vector2 position = topLeftGUIPosition - 0.5f * size;
            return new(position, size);
        }
        protected override string GetTooltipForElement(Collider element)
        {
            return element.name;
        }
    }
}