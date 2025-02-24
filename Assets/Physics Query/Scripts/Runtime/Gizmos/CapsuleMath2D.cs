using UnityEngine;

namespace PQuery
{
    public struct CapsuleMath2D
    {
        public static Vector2 TransformSize(Matrix4x4 transformation, Vector2 size)
        {
            return new(TransformSize(transformation, size, 0), TransformSize(transformation, size, 1));
        }
        private static float TransformSize(Matrix4x4 transformation, Vector2 size, int dimension)
        {
            return size[dimension] * GetSizeScale(transformation, dimension);
        }
        private static float GetSizeScale(Matrix4x4 transformation, int dimension)
        {
            Vector3 vector = transformation.GetColumn(dimension);
            Vector3 projectedVector = Vector3.ProjectOnPlane(vector, Vector3.forward);
            return projectedVector.magnitude;
        }
    }
}