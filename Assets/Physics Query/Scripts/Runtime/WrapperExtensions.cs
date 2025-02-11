using UnityEngine;

namespace PQuery
{
    public static class WrapperExtensions
    {
        public static VectorWrapper2D Wrap(this Vector2 vector)
        {
            return new(vector);
        }
        public static VectorWrapper3D Wrap3D(this Vector2 vector)
        {
            return new(vector);
        }
        public static VectorWrapper3D Wrap(this Vector3 vector)
        {
            return new(vector);
        }
        public static VectorWrapper2D Wrap2D(this Vector3 vector)
        {
            return new(vector);
        }

        public static RayWrapper2D Wrap(this Ray2D ray)
        {
            return new(ray);
        }
        public static RayWrapper3D Wrap(this Ray ray)
        {
            return new(ray);
        }
    }
}