using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class ScenePreview
    {
        private GUIStyle Style => _style ??= new(EditorStyles.miniButtonMid);
        private GUIStyle _style;

        protected void DrawLabel(Rect position, int index)
        {
            Handles.BeginGUI();
            GUI.Button(position, CreateContentForIndex(index), Style);
            Handles.EndGUI();
        }
        protected Vector2 GetLabelSize(int index)
        {
            GUIContent content = CreateContentForIndex(index);
            return Style.CalcSize(content);
        }
        protected GUIContent CreateContentForIndex(int index)
        {
            return new(index.ToString());
        }

        public abstract void DrawSceneGUI(GizmoPreview gizmo);
    }
    public abstract class ScenePreview<TElement> : ScenePreview
    {
        public override void DrawSceneGUI(GizmoPreview gizmo)
        {
            Result<TElement> result = GetResult(gizmo);
            for (int i = 0; i < result.Count; i++)
            {
                Rect position = GetLabelPositionForElement(result[i], i);
                DrawLabel(position, i);
            }
        }

        protected abstract Result<TElement> GetResult(GizmoPreview gizmo);
        protected abstract Rect GetLabelPositionForElement(TElement element, int index);
    }

    public class ScenePreview_Cast : ScenePreview<RaycastHit>
    {
        private const float ButtonHeight = 20;

        protected override Result<RaycastHit> GetResult(GizmoPreview gizmo)
        {
            return gizmo.CastResult;
        }
        protected override Rect GetLabelPositionForElement(RaycastHit element, int index)
        {
            Vector3 center = element.point;
            Vector3 offset = 2 * GizmoShape.HitSphereRadius * Vector3.up;
            Vector3 bottomEdgeWorldPosition = center + offset;

            Vector2 bottomLeftGUIPosition = HandleUtility.WorldToGUIPoint(bottomEdgeWorldPosition);
            Vector2 topLeftGUIPosition = bottomLeftGUIPosition + Vector2.down * ButtonHeight;
            Vector2 size = GetLabelSize(index);
            Vector2 position = topLeftGUIPosition + 0.5f * size.x * Vector2.left;

            return new(position, size);
        }
    }
    public class ScenePreview_Overlap : ScenePreview<Collider>
    {
        protected override Result<Collider> GetResult(GizmoPreview gizmo)
        {
            return gizmo.OverlapResult;
        }
        protected override Rect GetLabelPositionForElement(Collider element, int index)
        {
            Vector2 size = GetLabelSize(index);
            Vector2 topLeftGUIPosition = HandleUtility.WorldToGUIPoint(element.transform.position);
            Vector2 position = topLeftGUIPosition - 0.5f * size;
            return new(position, size);
        }
    }
}