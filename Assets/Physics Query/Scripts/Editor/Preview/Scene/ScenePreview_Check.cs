namespace PQuery.Editor
{
    public class ScenePreview_Check : ScenePreview
    {
        public override void DrawSceneGUI(PhysicsQuery query)
        {
            // Nothing to draw - no buttons that can click on anything in scene
        }
        protected override SceneButtonStrategy GetButtonStrategy()
        {
            return new SceneButtonStrategy_Collider();
        }
    }
}