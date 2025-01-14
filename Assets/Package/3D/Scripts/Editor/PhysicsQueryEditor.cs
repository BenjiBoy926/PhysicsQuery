using UnityEngine;
using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private const float MaxDistance = 1000;

        private PhysicsQuery _query;

        private void OnEnable()
        {
            _query = (PhysicsQuery)target;
        }
        private void OnSceneGUI()
        {
            Ray ray = _query.GetWorldRay();
            Vector3 start = ray.origin;
            Vector3 end = ray.GetPoint(GetMaxDistance());
            Handles.DrawLine(start, end);
        }
        private float GetMaxDistance()
        {
            return Mathf.Min(MaxDistance, _query.MaxDistance);
        }
    }
}