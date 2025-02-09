using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_CastNonAlloc : InspectorPreview_NonAlloc<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(PhysicsQuery3D query)
        {
            return query.CastNonAlloc(ResultSort3D.Distance);
        }
        protected override void DrawElementInspectorGUI(PhysicsQuery3D query, RaycastHit element, int index)
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

        private bool DrawFoldoutInspectorGUI(PhysicsQuery3D query, RaycastHit element, int index)
        {
            bool value = Preferences.GetRaycastHitFoldout(query, element.collider);
            string label = $"Element {index}";
            value = EditorGUILayout.Foldout(value, label);
            Preferences.SetRaycastHitFoldout(query, element.collider, value);
            return value;
        }
    }
}