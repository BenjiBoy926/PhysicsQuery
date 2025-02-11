using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public struct VectorWrapper2D : IVector<VectorWrapper2D>, IWrapper<Vector2>
    {
        public readonly float Magnitude => _value.magnitude;

        [SerializeField]
        private Vector2 _value;

        public VectorWrapper2D(Vector2 value)
        {
            _value = value;
        }

        public readonly VectorWrapper2D TransformAsPoint(Matrix4x4 matrix)
        {
            return new(matrix.MultiplyPoint3x4(_value));
        }
        public readonly VectorWrapper2D TransformAsDirection(Matrix4x4 matrix)
        {
            return new(matrix.MultiplyVector(_value));
        }
        public readonly VectorWrapper2D TransformAsScale(Matrix4x4 matrix)
        {
            Vector2 vector = Unwrap();
            Vector2 lossyScale = matrix.lossyScale;
            vector.x *= lossyScale.x;
            vector.y *= lossyScale.y;
            return vector.Wrap();
        }
        public readonly VectorWrapper2D Minus(VectorWrapper2D other)
        {
            return new(_value - other._value);
        }
        public readonly Vector2 Unwrap()
        {
            return _value;
        }
    }
}