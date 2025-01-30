using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    [CustomPropertyDrawer(typeof(PhysicsShapeProperty))]
    public class PhysicsShapePropertyDrawer : PropertyDrawer
    {
        private static readonly List<SerializedProperty> _subProperties = new(3);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var (type, shape) = GetTypeAndShape(property);
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, shape, label);
            if (EditorGUI.EndChangeCheck())
            {
                SetShape(shape, (PhysicsShapeType)type.enumValueIndex);
            }

            if (type.hasMultipleDifferentValues)
            {
                return;
            }
            position.y += position.height;
            position.y += EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.indentLevel++;
            DrawImmediateChildren(position, shape);
            EditorGUI.indentLevel--;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var (type, shape) = GetTypeAndShape(property);
            float height = EditorGUI.GetPropertyHeight(type, label, true);
            if (type.hasMultipleDifferentValues)
            {
                return height;
            }
            height += EditorGUIUtility.standardVerticalSpacing;
            List<SerializedProperty> immediateChildren = GetImmediateChildren(shape);
            for (int i = 0; i < immediateChildren.Count; i++)
            {
                height += EditorGUI.GetPropertyHeight(immediateChildren[i]);
                if (i < immediateChildren.Count - 1)
                {
                    height += EditorGUIUtility.standardVerticalSpacing;
                }
            }
            return height;
        }

        private (SerializedProperty, SerializedProperty) GetTypeAndShape(SerializedProperty property)
        {
            return (property.FindPropertyRelative(PhysicsShapeProperty.TypeFieldName), 
                property.FindPropertyRelative(PhysicsShapeProperty.ShapeFieldName));
        }
        private void SetShape(SerializedProperty shape, PhysicsShapeType type)
        {
            // Note: we have to construct a new managed reference per object being edited,
            // otherwise Unity prints an error
            SerializedObject parent = shape.serializedObject;
            Object[] targets = parent.targetObjects;
            for (int i = 0; i < targets.Length; i++)
            {
                SerializedObject individualObject = new(targets[i]);
                SerializedProperty individualReference = individualObject.FindProperty(shape.propertyPath);
                individualReference.managedReferenceValue = PhysicsShape.Create(type);
            }
        }
        private void DrawImmediateChildren(Rect position, SerializedProperty property)
        {
            List<SerializedProperty> subProperties = GetImmediateChildren(property);
            for (int i = 0; i < subProperties.Count; i++)
            {
                SerializedProperty subProperty = subProperties[i];
                position.height = EditorGUI.GetPropertyHeight(subProperty, true);
                EditorGUI.PropertyField(position, subProperty, true);
                position.y += position.height;
                position.y += EditorGUIUtility.standardVerticalSpacing;
            }
        }
        private List<SerializedProperty> GetImmediateChildren(SerializedProperty parent)
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
    }
}