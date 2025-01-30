using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PQuery.Editor
{
    public abstract class InspectorPreview
    {
        public abstract void DrawInspectorGUI(PhysicsQuery query);

        protected void DrawEachPropertyInspectorGUI(RaycastHit hit)
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