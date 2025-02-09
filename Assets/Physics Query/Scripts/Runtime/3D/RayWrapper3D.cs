using System.Collections;
using UnityEngine;

namespace PQuery
{
    public struct RayWrapper3D : IRay<VectorWrapper3D>
    {
        public VectorWrapper3D Origin
        {
            get => _value.origin.Wrap();
            set => _value.origin = value.Unwrap();
        }
        public VectorWrapper3D Direction
        {
            get => _value.direction.Wrap();
            set => _value.direction = value.Unwrap();
        }

        private Ray _value;

        public RayWrapper3D(Ray value)
        {
            _value = value;
        }

        public VectorWrapper3D GetPoint(float distance)
        {
            return _value.GetPoint(distance).Wrap();
        }
        public Ray Unwrap()
        {
            return _value;
        }
    }
}