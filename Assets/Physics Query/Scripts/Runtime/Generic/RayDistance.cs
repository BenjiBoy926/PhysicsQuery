namespace PQuery
{
    public readonly struct RayDistance<TVector, TRay>
        where TVector : IVector<TVector>
        where TRay : IRay<TVector>
    {
        public TVector Start => Ray.Origin;
        public TVector End => Ray.GetPoint(Distance);
        public TVector Direction => Ray.Direction;

        public readonly TRay Ray;
        public readonly float Distance;

        public RayDistance(TVector start, TVector end)
        {
            TVector offset = end.Minus(start);
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