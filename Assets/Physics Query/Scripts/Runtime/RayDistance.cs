using UnityEngine;

namespace PQuery
{
    public readonly struct RayDistance
    {
        public Vector3 Start => Ray.origin;
        public Vector3 End => Ray.GetPoint(Distance);
        public Vector3 Direction => Ray.direction;

        public readonly Ray Ray;
        public readonly float Distance;

        public RayDistance(Vector3 start, Vector3 end)
        {
            Vector3 offset = end - start;
            Ray = new Ray(start, offset);
            Distance = offset.magnitude;
        }
        public RayDistance(Ray ray, float distance)
        {
            Ray = ray;
            Distance = distance;
        }

        public Vector3 GetPoint(float distance)
        {
            return Ray.GetPoint(distance);
        }
    }
}