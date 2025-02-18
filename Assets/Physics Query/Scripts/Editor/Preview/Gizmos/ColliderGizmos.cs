using UnityEngine;

namespace PQuery.Editor
{
    public static class ColliderGizmos
    {
        public static void DrawGizmos(Component collider)
        {
            if (collider is Collider collider3D)
            {
                ColliderGizmos3D.DrawGizmos(collider3D);
            }
        }
    }
}