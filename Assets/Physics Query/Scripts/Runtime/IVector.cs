using UnityEngine;

namespace PQuery
{
    public interface IVector<TSelf> where TSelf : IVector<TSelf>
    {
        TSelf TransformAsPointBy(Matrix4x4 matrix);
    }
}