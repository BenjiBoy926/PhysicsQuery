using UnityEngine;

namespace PQuery.Editor
{
    public struct ColliderGizmos
    {
        public static void DrawGizmos(Component collider)
        {
            if (collider is Collider collider3D)
            {
                ColliderGizmos3D.DrawGizmos(collider3D);
            }
            else if (collider is Collider2D collider2D)
            {
                ColliderGizmos2D.DrawGizmos(collider2D);
            }
        }
    }
}