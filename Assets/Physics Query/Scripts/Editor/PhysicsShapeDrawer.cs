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
        private static string[] Labels => _labels ??= CreateShapeLabels();

        private readonly static List<Type> _shapes = new()
        {
            typeof(PhysicsShape_Box), 
            typeof(PhysicsShape_Capsule), 
            typeof(PhysicsShape_Ray), 
            typeof(PhysicsShape_Sphere)
        };
        private static string[] _labels;
        private static readonly Type[] _noArgs = new Type[0];

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.managedReferenceValue == null)
            {
                SetIndex(property, 0);
            }
            int current = GetIndex(property);
            current = EditorGUI.Popup(position, current, Labels);
            SetIndex(property, current);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        private static string[] CreateShapeLabels()
        {
            string[] labels = new string[_shapes.Count];
            string prefix = $"{nameof(PhysicsShape)}_";
            int prefixLength = prefix.Length; 
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = _shapes[i].Name[prefixLength..];
            }
            return labels;
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