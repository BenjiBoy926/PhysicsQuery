using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PQuery.Editor
{
    [CustomPropertyDrawer(typeof(PhysicsShape))]
    public class PhysicsShapeDrawer : PropertyDrawer
    {
        private static GUIContent[] Labels => _labels ??= CreateShapeLabels();

        private readonly static List<Type> _shapes = new()
        {
            typeof(PhysicsShape_Box), 
            typeof(PhysicsShape_Capsule), 
            typeof(PhysicsShape_Ray), 
            typeof(PhysicsShape_Sphere)
        };
        private static GUIContent[] _labels;
        private static readonly Type[] _noArgs = new Type[0];
        private static readonly List<SerializedProperty> _subProperties = new(3);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.managedReferenceValue == null)
            {
                SetIndex(property, 0);
            }
            position.height = EditorGUIUtility.singleLineHeight;
            DrawPopup(position, property, label);
            position.y += position.height;
            position.y += EditorGUIUtility.standardVerticalSpacing;
            if (!IsEachReferenceTheSameType(property))
            {
                return;
            }
            using (new EditorGUI.IndentLevelScope())
            {
                DrawSubProperties(position, property);
            }
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

        private static GUIContent[] CreateShapeLabels()
        {
            GUIContent[] labels = new GUIContent[_shapes.Count];
            string prefix = $"{nameof(PhysicsShape)}_";
            int prefixLength = prefix.Length; 
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = new(_shapes[i].Name[prefixLength..]);
            }
            return labels;
        }
        private void DrawPopup(Rect position, SerializedProperty property, GUIContent label)
        {
            int oldIndex = GetIndex(property);
            int newIndex = EditorGUI.Popup(position, label, oldIndex, Labels);
            if (oldIndex != newIndex)
            {
                SetIndex(property, newIndex);
            }
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
            return _shapes.IndexOf(property.managedReferenceValue.GetType());
        }
        private void SetIndex(SerializedProperty property, int index)
        {
            ConstructorInfo constructor = _shapes[index].GetConstructor(_noArgs);
            Object[] targets = property.serializedObject.targetObjects;
            // Note: you have to construct a new managed reference for EACH target during multi-editing or else Unity will print an error to the console
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