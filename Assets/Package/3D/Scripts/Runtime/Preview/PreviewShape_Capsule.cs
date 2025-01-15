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
            DrawShape(GetWorldCenter(), color);
        }
        protected override void DrawShape(Vector3 center, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(center, Query.Radius);
            Gizmos.DrawWireSphere(center + GetOtherCapOffset(), Query.Radius);
        }

        private Vector3 GetWorldCenter()
        {
            Vector3 cap1 = Query.GetWorldOrigin();
            Vector3 cap2 = Query.GetOtherCapWorldPosition();
            return Vector3.Lerp(cap1, cap2, 0.5f);
        }
        private Vector3 GetOtherCapOffset()
        {
            return Query.GetOtherCapWorldPosition() - Query.GetWorldOrigin();
        }
    }
}