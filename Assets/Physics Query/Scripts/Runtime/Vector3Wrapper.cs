using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public struct Vector3Wrapper : IVector<Vector3Wrapper>
    {
        public readonly float Magnitude => _value.magnitude;

        [SerializeField]
        private Vector3 _value;

        public Vector3Wrapper(Vector3 value)
        {
            _value = value;
        }

        public readonly Vector3Wrapper TransformAsPoint(Matrix4x4 matrix)
        {
            Vector3 vector = Unwrap();
            vector = matrix.MultiplyPoint3x4(vector);
            return vector.Wrap();
        }
        public readonly Vector3Wrapper TransformAsDirection(Matrix4x4 matrix)
        {
            Vector3 vector = Unwrap();
            vector = matrix.MultiplyVector(vector);
            return vector.Wrap();
        }
        public readonly Vector3Wrapper TransformAsScale(Matrix4x4 matrix)
        {
            Vector3 vector = Unwrap();
            Vector3 lossyScale = matrix.lossyScale;
            vector.x *= lossyScale.x;
            vector.y *= lossyScale.y;
            vector.z *= lossyScale.z;
            return vector.Wrap();
        }
        public Vector3Wrapper Subtract(Vector3Wrapper other)
        {
            return (Unwrap() - other.Unwrap()).Wrap();
        }
        public readonly Vector3 Unwrap()
        {
            return _value;
        }
    }
}