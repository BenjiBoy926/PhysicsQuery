using UnityEngine;
using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(RayQuery))]
    public class RayQueryEditor : UnityEditor.Editor
    {
        private const float MaxDistance = 1000;

        private RayQuery _query;

        private void OnEnable()
        {
            _query = (RayQuery)target;
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