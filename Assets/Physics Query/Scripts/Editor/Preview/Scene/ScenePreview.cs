using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class ScenePreview
    {
        public delegate void ColliderClickHandler(Collider collider);
        public static event ColliderClickHandler ColliderClicked = delegate { };

        protected SceneButtonStrategy ButtonStrategy => _buttonStrategy ??= GetButtonStrategy();
        private SceneButtonStrategy _buttonStrategy;

        protected bool DrawButton(object value, string label)
        {
            return ButtonStrategy.Draw(value, label);
        }
        protected static void ClickCollider(Collider collider)
        {
            EditorGUIUtility.PingObject(collider);
            ColliderClicked(collider);
        }

        public abstract void DrawSceneGUI(PhysicsQuery query);
        protected abstract SceneButtonStrategy GetButtonStrategy();
    }
}