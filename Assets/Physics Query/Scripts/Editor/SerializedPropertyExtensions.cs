using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace PQuery.Editor
{
    public static class SerializedPropertyExtensions
    {
        public static float GetChildrenHeight(this SerializedProperty property)
        {
            float height = 0f;
            foreach (SerializedProperty child in property.GetImmediateChildren())
            {
                height += EditorGUI.GetPropertyHeight(child, true);
            }
            return height;
        }
        public static IEnumerable<SerializedProperty> GetImmediateChildren(this SerializedProperty property)
        {
            SerializedProperty iterator = property.Copy();
            SerializedProperty end = property.GetEndProperty();
            if (!iterator.NextVisible(true))
            {
                yield break;
            }
            while (!SerializedProperty.EqualContents(iterator, end))
            {
                yield return iterator.Copy();
                iterator.NextVisible(false);
            }
        }
    }
}