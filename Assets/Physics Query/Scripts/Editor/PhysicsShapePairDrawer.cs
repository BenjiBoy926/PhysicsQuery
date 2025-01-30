using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    [CustomPropertyDrawer(typeof(PhysicsShapePair))]
    public class PhysicsShapePairDrawer : PropertyDrawer
    {
        private static readonly List<SerializedProperty> _subProperties = new(3);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var (type, shape) = GetTypeAndShape(property);
            EditorGUI.BeginChangeCheck();
            position = PropertyField(position, type, label);
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                SetShape(shape, (PhysicsShapeType)type.enumValueIndex);
            }

            if (type.hasMultipleDifferentValues)
            {
                return;
            }
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
            return (property.FindPropertyRelative(PhysicsShapePair.TypeFieldName), 
                property.FindPropertyRelative(PhysicsShapePair.ShapeFieldName));
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
                individualReference.managedReferenceValue = PhysicsShapePair.CreateShape(type);
                individualObject.ApplyModifiedProperties();
                individualObject.Update();
            }
        }
        private void DrawImmediateChildren(Rect position, SerializedProperty property)
        {
            List<SerializedProperty> immediateChildren = GetImmediateChildren(property);
            for (int i = 0; i < immediateChildren.Count; i++)
            {
                SerializedProperty subProperty = immediateChildren[i];
                position = PropertyField(position, subProperty, null);
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