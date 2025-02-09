using UnityEngine;

namespace PQuery
{
    public interface IVector<TSelf> where TSelf : IVector<TSelf>
    {
        float Magnitude { get; }
        TSelf TransformAsPoint(Matrix4x4 matrix);
        TSelf TransformAsDirection(Matrix4x4 matrix);
        TSelf TransformAsScale(Matrix4x4 matrix);
        TSelf Subtract(TSelf other);
    }
}