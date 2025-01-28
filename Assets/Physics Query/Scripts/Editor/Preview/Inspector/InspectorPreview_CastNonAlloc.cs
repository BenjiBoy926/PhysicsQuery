using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_CastNonAlloc : InspectorPreview_NonAlloc<RaycastHit>
    {
        private readonly Dictionary<int, bool> _foldout = new();

        protected override Result<RaycastHit> GetResult(PhysicsQuery query)
        {
            return query.CastNonAlloc(ResultSort.Distance);
        }
        protected override void DrawElementInspectorGUI(RaycastHit element, int index)
        {
            EditorGUI.indentLevel++;
            DrawFoldoutInspectorGUI(element, index);
            if (GetFoldoutValue(element))
            {
                EditorGUI.indentLevel++;
                DrawEachPropertyInspectorGUI(element);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        public override void HighlightElement(object element)
        {
            RaycastHit hit = CastHighlightElement<RaycastHit>(element);
            CollapseAllOtherFoldouts(hit);
            EditorGUIUtility.PingObject(hit.collider);
        }

        private void DrawFoldoutInspectorGUI(RaycastHit element, int index)
        {
            bool value = GetFoldoutValue(element);
            string label = $"Element {index}";
            value = EditorGUILayout.Foldout(value, label);
            SetFoldoutValue(element, value);
        }
        private void CollapseAllOtherFoldouts(RaycastHit hit)
        {
            List<int> keys = new(_foldout.Keys);
            int foldedOut = GetFoldoutID(hit);
            for (int i = 0; i < keys.Count; i++)
            {
                int key = keys[i];
                _foldout[key] = key == foldedOut;
            }
        }
        private bool GetFoldoutValue(RaycastHit hit)
        {
            return _foldout.GetValueOrDefault(GetFoldoutID(hit));
        }
        private void SetFoldoutValue(RaycastHit hit, bool value)
        {
            _foldout[GetFoldoutID(hit)] = value;
        }
        private int GetFoldoutID(RaycastHit hit)
        {
            return hit.colliderInstanceID;
        }
    }
}