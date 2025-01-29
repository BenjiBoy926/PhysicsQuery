using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

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

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.managedReferenceValue == null)
            {
                SetIndex(property, 0);
            }
            DrawPopup(position, property, label);
            position.y += EditorGUIUtility.singleLineHeight;
            DrawSubProperties(position, property);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
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
            SerializedProperty iterator = property.Copy();
            SerializedProperty end = property.Copy();
            bool gotChildProperty = iterator.NextVisible(true);
            bool gotEndProperty = end.NextVisible(false);
            if (!gotChildProperty && !gotEndProperty)
            {
                return;
            }
            while (!SerializedProperty.EqualContents(iterator, end))
            {
                EditorGUI.PropertyField(position, iterator, true);
                position.y += EditorGUI.GetPropertyHeight(iterator, true);
                iterator.NextVisible(false);
            }
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