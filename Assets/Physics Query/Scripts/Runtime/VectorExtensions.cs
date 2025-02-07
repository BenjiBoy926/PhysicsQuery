using UnityEngine;

namespace PQuery
{
    public static class VectorExtensions
    {
        public static Vector3D ToPQuery(this Vector3 vector)
        {
            return new(vector);
        }
    }
}