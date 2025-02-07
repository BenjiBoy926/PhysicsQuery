using System;
using UnityEngine;

namespace PQuery
{
    [Serializable]
    public struct Vector3D : IVector<Vector3D>
    {
        [SerializeField]
        private float _x;
        [SerializeField] 
        private float _y;
        [SerializeField]
        private float _z;

        public Vector3D(Vector3 other)
        {
            _x = other.x;
            _y = other.y;
            _z = other.z;
        }
        public Vector3D(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public readonly Vector3D TransformAsPointBy(Matrix4x4 matrix)
        {
            Vector3 vector = ToUnity();
            vector = matrix.MultiplyPoint3x4(vector);
            return vector.ToPQuery();
        }
        public readonly Vector3D TransformAsDirectionBy(Matrix4x4 matrix)
        {
            Vector3 vector = ToUnity();
            vector = matrix.MultiplyVector(vector);
            return vector.ToPQuery();
        }
        public readonly Vector3D TransformAsScaleBy(Matrix4x4 matrix)
        {
            Vector3 vector = ToUnity();
            Vector3 lossyScale = matrix.lossyScale;
            vector.x *= lossyScale.x;
            vector.y *= lossyScale.y;
            vector.z *= lossyScale.z;
            return vector.ToPQuery();
        }
        public readonly Vector3 ToUnity()
        {
            return new(_x, _y, _z);
        }
    }
}