using UnityEngine;

namespace PQuery
{
    public struct RayDistance : IRayDistance<Vector3Wrapper>
    {
        public Vector3 Start => Ray.origin;
        public Vector3 End => Ray.GetPoint(Distance);
        public Vector3 Direction => Ray.direction;

        public Ray Ray;
        public float Distance;

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

        public void SetStartAndEnd(Vector3Wrapper start, Vector3Wrapper end)
        {
            RayDistance copy = new(start.Unwrap(), end.Unwrap());
            Ray = copy.Ray;
            Distance = copy.Distance;
        }
        public Vector3 GetPoint(float distance)
        {
            return Ray.GetPoint(distance);
        }
    }
}