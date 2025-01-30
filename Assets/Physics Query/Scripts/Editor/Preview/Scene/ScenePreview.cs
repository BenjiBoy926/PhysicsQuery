using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class ScenePreview
    {
        public delegate void ColliderClickHandler(Collider collider);
        public static event ColliderClickHandler ColliderClicked = delegate { };

        protected static void ClickCollider(Collider collider)
        {
            EditorGUIUtility.PingObject(collider);
            ColliderClicked(collider);
        }

        public abstract void DrawSceneGUI(PhysicsQuery query);
    }
}