using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
            DrawPopup(position, property, label);
            position.y += EditorGUIUtility.singleLineHeight;
            position.y += EditorGUIUtility.standardVerticalSpacing;
            DrawSubProperties(position, property);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            List<SerializedProperty> subProperties = GetSubProperties(property);
            for (int i = 0; i < subProperties.Count; i++)
            {
                height += EditorGUI.GetPropertyHeight(subProperties[i]);
                height += EditorGUIUtility.standardVerticalSpacing;
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
            int current = GetIndex(property);
            current = EditorGUI.Popup(position, label, current, Labels);
            SetIndex(property, current);
        }
        private void DrawSubProperties(Rect position, SerializedProperty property)
        {
            List<SerializedProperty> subProperties = GetSubProperties(property);
            for (int i = 0; i < subProperties.Count; i++)
            {
                SerializedProperty subProperty = subProperties[i];
                EditorGUI.PropertyField(position, subProperty, true);
                position.y += EditorGUI.GetPropertyHeight(subProperty);
                position.y += EditorGUIUtility.standardVerticalSpacing;
            }
        }
        private List<SerializedProperty> GetSubProperties(SerializedProperty parent)
        {
            _subProperties.Clear();
            SerializedProperty iterator = parent.Copy();
            SerializedProperty end = parent.Copy();
            bool gotChildProperty = iterator.NextVisible(true);
            bool gotEndProperty = end.NextVisible(false);
            if (!gotChildProperty && !gotEndProperty)
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
            property.managedReferenceValue = constructor.Invoke(null);
        }
    }
}