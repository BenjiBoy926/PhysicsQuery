using UnityEngine;

namespace PhysicsQuery
{
    public class GizmoShape_Empty : GizmoShape<EmptyQuery>
    {
        public GizmoShape_Empty(EmptyQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape()
        {

        }
        protected override void DrawShape(Vector3 center)
        {

        }
    }
}