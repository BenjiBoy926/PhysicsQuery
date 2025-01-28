using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PQuery.Editor
{
    public class InspectorPreview_CastNonAlloc : InspectorPreview<RaycastHit>
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
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            if (element is not RaycastHit hit)
            {
                throw new ArgumentException($"Expected {element} to have the type {typeof(RaycastHit)}, but it has the type {element.GetType().Name}");
            }
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