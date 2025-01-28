using UnityEditor;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace PQuery.Editor
{
    public class InspectorPreview_OverlapNonAlloc : InspectorPreview<Collider>
    {
        protected override void DrawElementInspectorGUI(Collider element, int index)
        {
            string label = $"Element {index}";
            EditorGUILayout.ObjectField(label, element, element.GetType(), true);
        }
        protected override Result<Collider> GetResult(PhysicsQuery query)
        {
            return query.OverlapNonAlloc();
        }
        public override void HighlightElement(object element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            Type elementType = element.GetType();
            if (!elementType.IsSubclassOf(typeof(Object)))
            {
                throw new ArgumentException($"Expected {element} to be a subtype of {typeof(Object).Name}, but it has the type {element.GetType()}");
            }
            EditorGUIUtility.PingObject(element as Object);
        }
    }
}