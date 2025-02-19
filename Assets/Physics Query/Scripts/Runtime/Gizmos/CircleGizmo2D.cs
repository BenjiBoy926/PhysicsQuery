using System.Collections;
using UnityEngine;

namespace PQuery
{
    public static class CircleGizmo2D
    {
        public static void Draw(Vector2 center, float radius)
        {
            Vector2 right = Vector2.right * radius;
            Vector2 up = Vector2.up * radius;
            new EllipseGizmo(center, right, up).Draw();
        }
    }
}