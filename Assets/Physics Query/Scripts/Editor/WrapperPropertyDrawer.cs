using UnityEditor;
using UnityEngine;
using System.Linq;

namespace PQuery.Editor
{
    [CustomPropertyDrawer(typeof(VectorWrapper2D))]
    [CustomPropertyDrawer(typeof(VectorWrapper3D))]
    public class WrapperPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty first = property.GetImmediateChildren().First();
            EditorGUI.PropertyField(position, first, label, true);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty first = property.GetImmediateChildren().First();
            return EditorGUI.GetPropertyHeight(first, label, true);
        }
    }
}