using UnityEngine;

namespace PQuery
{
    public static class WrapperExtensions
    {
        public static VectorWrapper3D Wrap(this Vector3 vector)
        {
            return new(vector);
        }
        public static RayWrapper3D Wrap(this Ray ray)
        {
            return new(ray);
        }
    }
}