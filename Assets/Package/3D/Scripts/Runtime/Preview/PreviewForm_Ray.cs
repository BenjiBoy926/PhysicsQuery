using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewForm_Ray : PreviewForm<RayQuery>
    {
        public PreviewForm_Ray(RayQuery query) : base(query)
        {
        }

        public override void DrawCastGizmos()
        {
            int hitCount = Query.Cast(out RaycastHit[] hits);
            if (hitCount > 0)
            {
                DrawCastResults(hits, hitCount);
            }
            else
            {
                DrawDefaultLine();
            }
        }
        public override void DrawOverlapGizmos()
        {
            int overlapCount = Query.Overlap(out Collider[] colliders);
            if (overlapCount > 0)
            {
                // TODO: draw an outline of the colliders
                DrawDefaultLine(Color.green);
            }
            else
            {
                DrawDefaultLine();
            }
        }
    }
}