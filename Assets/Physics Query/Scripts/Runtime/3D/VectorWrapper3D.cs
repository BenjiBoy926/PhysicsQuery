using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public struct VectorWrapper3D : IVector<VectorWrapper3D>, IWrapper<Vector3>
    {
        public readonly float Magnitude => _value.magnitude;

        [SerializeField]
        private Vector3 _value;

        public VectorWrapper3D(Vector3 value)
        {
            _value = value;
        }

        public readonly VectorWrapper3D TransformAsPoint(Matrix4x4 matrix)
        {
            return new(matrix.MultiplyPoint3x4(_value));
        }
        public readonly VectorWrapper3D TransformAsDirection(Matrix4x4 matrix)
        {
            return new(matrix.MultiplyVector(_value));
        }
        public readonly VectorWrapper3D TransformAsScale(Matrix4x4 matrix)
        {
            Vector3 vector = Unwrap();
            Vector3 lossyScale = matrix.lossyScale;
            vector.x *= lossyScale.x;
            vector.y *= lossyScale.y;
            vector.z *= lossyScale.z;
            return vector.Wrap();
        }
        public readonly VectorWrapper3D Minus(VectorWrapper3D other)
        {
            return new(_value - other._value);
        }
        public readonly Vector3 Unwrap()
        {
            return _value;
        }
    }
}