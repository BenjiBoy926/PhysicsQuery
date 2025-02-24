using UnityEngine;

namespace PQuery
{
    public struct CapsuleGizmo3D
    {
        public static void Draw(Vector3 center, Vector3 axis, float radius)
        {
            if (axis.sqrMagnitude < 1E-6f)
            {
                Gizmos.DrawWireSphere(center, radius);
                return;
            }

            Vector3 topCapCenter = center + axis;
            Vector3 bottomCapCenter = center - axis;

            GetBasisVectors(axis, out Vector3 right, out Vector3 forward);
            right = right.normalized * radius;
            forward = forward.normalized * radius;

            Vector3 left = -right;
            Vector3 down = -axis;
            Vector3 back = -forward;
            Vector3 hemisphereUp = axis.normalized * radius;
            Vector3 hemisphereDown = -hemisphereUp;

            DrawHemisphere(topCapCenter, right, hemisphereUp, forward);
            DrawHemisphere(bottomCapCenter, right, hemisphereDown, forward);
            Gizmos.DrawLine(center + forward + axis, center + forward + down);
            Gizmos.DrawLine(center + back + axis, center + back + down);
            Gizmos.DrawLine(center + right + axis, center + right + down);
            Gizmos.DrawLine(center + left + axis, center + left + down);
        }
        private static void DrawHemisphere(Vector3 center, Vector3 right, Vector3 up, Vector3 forward)
        {
            new EllipseGizmo(center, forward, right).Draw();
            new EllipseGizmo(center, forward, up).DrawHalf();
            new EllipseGizmo(center, right, up).DrawHalf();
        }
        private static void GetBasisVectors(Vector3 up, out Vector3 right, out Vector3 forward)
        {
            Vector3 cross = GetArbitraryCrossAxis(up);
            right = Vector3.Cross(cross, up);
            forward = Vector3.Cross(up, right);
        }
        private static Vector3 GetArbitraryCrossAxis(Vector3 initial)
        {
            return Colinear(initial, Vector3.up) ? Vector3.right : Vector3.up;
        }
        private static bool Colinear(Vector3 a, Vector3 b)
        {
            float angle = Vector3.Angle(a, b);
            return angle < 0.01f || angle > 179.99f;
        }
    }
}