using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class InspectorPreview_CastNonAlloc : InspectorPreview_NonAlloc<BoxedRaycastHit>
    {
        protected override Result<BoxedRaycastHit> GetResult(PhysicsQuery query)
        {
            return query.BoxedCastNonAlloc(ResultSortBoxed.Distance);
        }
        protected override void DrawElementInspectorGUI(PhysicsQuery query, BoxedRaycastHit element, int index)
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

        private bool DrawFoldoutInspectorGUI(PhysicsQuery query, BoxedRaycastHit element, int index)
        {
            bool value = Preferences.GetRaycastHitFoldout(query, element.Collider);
            string label = $"Element {index}";
            value = EditorGUILayout.Foldout(value, label);
            Preferences.SetRaycastHitFoldout(query, element.Collider, value);
            return value;
        }
    }
}