using UnityEngine;

namespace PQuery
{
    public struct CircleGizmo2D
    {
        public static void Draw(Matrix4x4 transformation, Vector3 localPosition, float localRadius)
        {

        }
        public static void Draw(Vector3 worldCenter, float worldRadius)
        {
            Vector2 right = Vector2.right * worldRadius;
            Vector2 up = Vector2.up * worldRadius;
            EllipseGizmo.Draw(worldCenter, right, up);
        }
    }
}