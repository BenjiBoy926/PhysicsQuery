using UnityEngine;

namespace PQuery
{
    public interface IVector<TSelf> where TSelf : IVector<TSelf>
    {
        TSelf TransformAsPoint(Matrix4x4 matrix);
    }
}