using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PQuery.Editor
{
    [CustomPropertyDrawer(typeof(SubtypeDropdownAttribute))]
    public class SubtypeDropdownDrawer : PropertyDrawer
    {
        private List<Type> Subtypes => _subtypes ??= CreateSubtypeList();
        private GUIContent[] Labels => _labels ??= CreateShapeLabels();

        private static readonly Type[] _noArgs = new Type[0];
        private readonly List<SerializedProperty> _subProperties = new(3);
        private List<Type> _subtypes;
        private GUIContent[] _labels;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.managedReferenceValue == null)
            {
                SetIndex(property, 0);
            }

            bool oldShowMixedValue = EditorGUI.showMixedValue;
            EditorGUI.showMixedValue = !IsEachReferenceTheSameType(property);

            position = DrawPopup(position, property, label);
            if (!EditorGUI.showMixedValue)
            {
                EditorGUI.indentLevel++;
                DrawSubProperties(position, property);
                EditorGUI.indentLevel--;
            }

            EditorGUI.showMixedValue = oldShowMixedValue;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            if (!IsEachReferenceTheSameType(property))
            {
                return height;
            }
            List<SerializedProperty> subProperties = GetSubProperties(property);
            for (int i = 0; i < subProperties.Count; i++)
            {
                height += EditorGUI.GetPropertyHeight(subProperties[i]);
                if (i < subProperties.Count - 1)
                {
                    height += EditorGUIUtility.standardVerticalSpacing;
                }
            }
            return height;
        }

        private List<Type> CreateSubtypeList()
        {
            List<Type> subtypes = new();
            Type baseType = fieldInfo.FieldType;
            Type[] allTypes = baseType.Assembly.GetTypes();
            for (int i = 0; i < allTypes.Length; i++)
            {
                if (allTypes[i].IsSubclassOf(baseType))
                {
                    subtypes.Add(allTypes[i]);
                }
            }
            return subtypes;
        }
        private GUIContent[] CreateShapeLabels()
        {
            GUIContent[] labels = new GUIContent[Subtypes.Count];
            string prefix = $"{fieldInfo.FieldType.Name}_";
            for (int i = 0; i < labels.Length; i++)
            {
                string subtypeName = Subtypes[i].Name;
                string labelText = subtypeName.Replace(prefix, string.Empty);
                labels[i] = new(labelText);
            }
            return labels;
        }
        private Rect DrawPopup(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.BeginChangeCheck();
            int index = EditorGUI.Popup(position, label, GetIndex(property), Labels);
            if (EditorGUI.EndChangeCheck())
            {
                SetIndex(property, index);
            }

            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            return position;
        }
        private void DrawSubProperties(Rect position, SerializedProperty property)
        {
            List<SerializedProperty> subProperties = GetSubProperties(property);
            for (int i = 0; i < subProperties.Count; i++)
            {
                position = PropertyField(position, subProperties[i], null);
            }
        }
        private static Rect PropertyField(Rect position, SerializedProperty property, GUIContent label)
        {
            label ??= new(property.displayName);
            position.height = EditorGUI.GetPropertyHeight(property, true);
            EditorGUI.PropertyField(position, property, label, true);
            position.y += position.height;
            position.y += EditorGUIUtility.standardVerticalSpacing;
            return position;
        }

        private List<SerializedProperty> GetSubProperties(SerializedProperty parent)
        {
            _subProperties.Clear();
            SerializedProperty iterator = parent.Copy();
            SerializedProperty end = parent.GetEndProperty();
            if (!iterator.NextVisible(true))
            {
                return _subProperties;
            }
            while (!SerializedProperty.EqualContents(iterator, end))
            {
                _subProperties.Add(iterator.Copy());
                iterator.NextVisible(false);
            }
            return _subProperties;
        }
        private int GetIndex(SerializedProperty property)
        {
            return Subtypes.IndexOf(property.managedReferenceValue.GetType());
        }
        private void SetIndex(SerializedProperty property, int index)
        {
            ConstructorInfo constructor = Subtypes[index].GetConstructor(_noArgs);
            Object[] targets = property.serializedObject.targetObjects;
            // Note: you have to construct a new managed reference for EACH target during multi-editing
            // or else Unity will print an error to the console.
            for (int i = 0; i < targets.Length; i++)
            {
                SerializedObject serializedTarget = new(targets[i]);
                SerializedProperty referenceProperty = serializedTarget.FindProperty(property.propertyPath);
                referenceProperty.managedReferenceValue = constructor.Invoke(null);
                serializedTarget.ApplyModifiedProperties();
            }
            property.serializedObject.Update();
        }
        private bool IsEachReferenceTheSameType(SerializedProperty property)
        {
            Object[] targets = property.serializedObject.targetObjects;
            if (targets.Length <= 1)
            {
                return true;
            }
            if (property.managedReferenceValue == null)
            {
                return false;
            }
            for (int i = 0; i < targets.Length; i++)
            {
                SerializedObject serializedTarget = new(targets[i]);
                SerializedProperty referenceProperty = serializedTarget.FindProperty(property.propertyPath);
                if (!IsEachReferenceTheSameType(property, referenceProperty))
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsEachReferenceTheSameType(SerializedProperty a, SerializedProperty b)
        {
            if (!IsManagedReference(a) || !IsManagedReference(b))
            {
                return false;
            }
            if (a.managedReferenceValue == null || b.managedReferenceValue == null)
            {
                return false;
            }
            return a.managedReferenceValue.GetType() == b.managedReferenceValue.GetType();
        }
        private bool IsManagedReference(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.ManagedReference;
        }
    }
}