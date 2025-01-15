using UnityEngine;

namespace PhysicsQuery
{
    public class PreviewShape_Box : PreviewShape<BoxQuery>
    {
        public PreviewShape_Box(BoxQuery query) : base(query)
        {
        }

        public override void DrawCastGizmos()
        {
            PhysicsCastResult result = Query.Cast();
            if (result.IsEmpty)
            {
                Vector3 start = Query.GetWorldOrigin();
                Vector3 end = GetEndPoint();
                DrawBox(start, Color.gray);
                DrawBox(end, Color.gray);
                DrawDefaultLine();
            }
            else
            {
                DrawCastResults(result);
                DrawBoxesFromResult(result);
                DrawBox(GetEndPoint(), Color.gray);
            }
        }
        public override void DrawOverlapGizmos()
        {
            PhysicsOverlapResult result = Query.Overlap();
            if (result.IsEmpty)
            {
                DrawBox(Query.GetWorldOrigin(), Color.gray);
            }
            else
            {
                DrawBox(Query.GetWorldOrigin(), Color.green);
            }
        }

        private void DrawBoxesFromResult(PhysicsCastResult result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                DrawBoxAtHit(result.Get(i));
            }
        }
        private void DrawBoxAtHit(RaycastHit hit)
        {
            DrawBox(GetBoxCenter(hit), Color.green);
        }
        private Vector3 GetBoxCenter(RaycastHit hit)
        {
            Ray worldRay = Query.GetWorldRay();
            return worldRay.GetPoint(hit.distance);
        }
        private void DrawBox(Vector3 center, Color color)
        {
            Quaternion worldOrientation = Query.GetWorldOrientation();
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(worldOrientation);
            center = rotationMatrix.inverse.MultiplyVector(center);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = color;
            Gizmos.DrawWireCube(center, Query.GetWorldSize());
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}