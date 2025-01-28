using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public abstract class ScenePreview
    {
        public delegate void ElementClickedHandler(object element);
        public event ElementClickedHandler ElementClicked = delegate { };

        protected SceneButtonStrategy ButtonStrategy => _buttonStrategy ??= GetButtonStrategy();
        private SceneButtonStrategy _buttonStrategy;

        protected void DrawButton(object value, string label)
        {
            if (ButtonStrategy.Draw(value, label))
            {
                NotifyElementClicked(value);
            }
        }
        protected void NotifyElementClicked(object element)
        {
            ElementClicked(element);
        }

        public abstract void DrawSceneGUI(PhysicsQuery query);
        protected abstract SceneButtonStrategy GetButtonStrategy();
    }
}