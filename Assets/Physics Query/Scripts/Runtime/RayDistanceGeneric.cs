namespace PQuery
{
    public class RayDistanceGeneric<TVector, TRay>
        where TVector : IVector<TVector>
        where TRay : IRay<TVector>
    {
        public TVector Start => Ray.Origin;
        public TVector End => Ray.GetPoint(Distance);
        public TVector Direction => Ray.Direction;

        public TRay Ray;
        public float Distance;

        public void SetStartAndEnd(TVector start, TVector end)
        {
            TVector offset = end.Subtract(start);
            Ray = default;
            Ray.Origin = start;
            Ray.Direction = offset;
            Distance = offset.Magnitude;
        }
        public TVector GetPoint(float distance)
        {
            return Ray.GetPoint(distance);
        }
    }
}