using System;
using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public struct HandlesMatrixScope : IDisposable
    {
        private Matrix4x4 _oldMatrix;

        public HandlesMatrixScope(Matrix4x4 newMatrix)
        {
            _oldMatrix = newMatrix;
            Handles.matrix = newMatrix;
        }
        public void Dispose()
        {
            Handles.matrix = _oldMatrix;
        }
    }
}