using UnityEngine;

namespace PQuery
{
    public static class WrapperExtensions
    {
        public static Vector3Wrapper Wrap(this Vector3 vector)
        {
            return new(vector);
        }
    }
}