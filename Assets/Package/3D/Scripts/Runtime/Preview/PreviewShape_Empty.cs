using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Empty : PreviewShape<EmptyQuery>
    {
        public PreviewShape_Empty(EmptyQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {

        }
        protected override void DrawShape(Vector3 center, Color color)
        {

        }
    }
}