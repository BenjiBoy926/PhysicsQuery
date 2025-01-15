using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Capsule : PreviewShape<CapsuleQuery>
    {
        public PreviewShape_Capsule(CapsuleQuery query) : base(query)
        {
        }

        protected override void DrawOverlapShape(Color color)
        {
            DrawShape(GetStartPosition(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            Vector3 offset = GetCapCenterOffset();
            Vector3 cap1 = center + offset;
            Vector3 cap2 = center - offset;

            Gizmos.color = color;
            Gizmos.DrawWireSphere(cap1, Query.Radius);
            Gizmos.DrawWireSphere(cap2, Query.Radius);
        }

        protected override Ray GetWorldRay()
        {
            Ray ray = base.GetWorldRay();
            ray.origin -= GetCapCenterOffset();
            return ray;
        }
        private Vector3 GetCapCenterOffset()
        {
            return (Query.GetWorldOrigin() - Query.GetOtherCapWorldPosition()) / 2;
        }
    }
}