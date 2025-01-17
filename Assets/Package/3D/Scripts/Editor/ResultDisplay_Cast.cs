using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class ResultDisplay_Cast : ResultDisplay<RaycastHit>
    {
        private readonly Dictionary<int, bool> _foldout = new();

        protected override Result<RaycastHit> GetResult(Preview preview)
        {
            return preview.CastResult;
        }
        protected override void DrawElementInspectorGUI(RaycastHit element, int index)
        {
            bool value = _foldout.GetValueOrDefault(element.colliderInstanceID);
            string label = $"Element {index}";
            _foldout[element.colliderInstanceID] = EditorGUILayout.Foldout(value, label);
            if (_foldout[element.colliderInstanceID])
            {
                DrawEachPropertyInspectorGUI(element);
            }
        }
        private void DrawEachPropertyInspectorGUI(RaycastHit hit)
        {
            PropertyInfo[] properties = hit.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                DrawPropertyInspectorGUI(hit, properties[i]);
            }
        }
        private void DrawPropertyInspectorGUI(RaycastHit instance, PropertyInfo property)
        {
            string label = ObjectNames.NicifyVariableName(property.Name);
            object value = property.GetValue(instance, null);

            if (property.PropertyType.IsSubclassOf(typeof(Object)))
            {
                EditorGUILayout.ObjectField(label, value as Object, property.PropertyType, true);
            }
            else if (property.PropertyType == typeof(float))
            {
                EditorGUILayout.FloatField(label, (float)value);
            }
            else if (property.PropertyType == typeof(int))
            {
                EditorGUILayout.IntField(label, (int)value);
            }
            else if (property.PropertyType == typeof(Vector2))
            {
                EditorGUILayout.Vector2Field(label, (Vector2)value);
            }
            else if (property.PropertyType == typeof(Vector3))
            {
                EditorGUILayout.Vector3Field(label, (Vector3)value);
            }
            else
            {
                EditorGUILayout.HelpBox($"Cannot display property '{property.Name}' because the method for displaying the type '{property.PropertyType.Name}' has not been implemented", MessageType.Error);
            }
        }
    }
}