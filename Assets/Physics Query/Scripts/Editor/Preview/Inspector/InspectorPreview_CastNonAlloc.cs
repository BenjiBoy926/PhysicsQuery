using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_CastNonAlloc : InspectorPreview_NonAlloc<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(PhysicsQuery query)
        {
            return query.CastNonAlloc(ResultSort.Distance);
        }
        protected override void DrawElementInspectorGUI(PhysicsQuery query, RaycastHit element, int index)
        {
            EditorGUI.indentLevel++;
            if (DrawFoldoutInspectorGUI(query, element, index))
            {
                EditorGUI.indentLevel++;
                DrawEachPropertyInspectorGUI(element);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }

        private bool DrawFoldoutInspectorGUI(PhysicsQuery query, RaycastHit element, int index)
        {
            bool value = Preferences.GetRaycastHitFoldout(query, element.collider);
            string label = $"Element {index}";
            value = EditorGUILayout.Foldout(value, label);
            Preferences.SetRaycastHitFoldout(query, element.collider, value);
            return value;
        }
    }
}