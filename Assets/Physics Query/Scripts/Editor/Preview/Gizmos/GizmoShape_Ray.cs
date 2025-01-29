using UnityEngine;

namespace PQuery.Editor
{
    public class GizmoShape_Ray : GizmoShape
    {
        protected override void DrawOverlapShape()
        {
            Gizmos.DrawLine(GetStartPosition(), GetEndPosition());
        }
        protected override void DrawShape(Vector3 center)
        {
            // No shapes to draw for raycasting
        }
    }
}