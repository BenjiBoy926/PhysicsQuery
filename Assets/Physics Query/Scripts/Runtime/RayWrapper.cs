using System.Collections;
using UnityEngine;

namespace PQuery
{
    public struct RayWrapper : IRay<Vector3Wrapper>
    {
        public Vector3Wrapper Origin
        {
            get => _value.origin.Wrap();
            set => _value.origin = value.Unwrap();
        }
        public Vector3Wrapper Direction
        {
            get => _value.direction.Wrap();
            set => _value.direction = value.Unwrap();
        }

        private Ray _value;

        public RayWrapper(Ray value)
        {
            _value = value;
        }

        public Vector3Wrapper GetPoint(float distance)
        {
            return _value.GetPoint(distance).Wrap();
        }
        public Ray Unwrap()
        {
            return _value;
        }
    }
}