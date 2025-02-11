using UnityEngine;

namespace PQuery
{
    public struct RayWrapper2D : IRay<VectorWrapper2D>, IWrapper<Ray2D>
    {
        public VectorWrapper2D Origin 
        {
            get => _ray.origin.Wrap();
            set => _ray.origin = value.Unwrap();
        }
        public VectorWrapper2D Direction 
        { 
            get => _ray.direction.Wrap();
            set => _ray.direction = value.Unwrap();
        }

        private Ray2D _ray;

        public RayWrapper2D(Ray2D ray)
        {
            _ray = ray;
        }

        public VectorWrapper2D GetPoint(float distance)
        {
            return _ray.GetPoint(distance).Wrap();
        }
        public readonly Ray2D Unwrap()
        {
            return _ray;
        }
    }
}