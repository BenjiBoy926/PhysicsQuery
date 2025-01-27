using PQuery;
using UnityEngine;
using UnityEngine.Profiling;
public class CastTest : MonoBehaviour
{
    [SerializeField]
    private PhysicsQuery _query;
    private void Update()
    {
        _query.CastNonAlloc(ResultSort.Distance);
        _query.OverlapNonAlloc();

        Profiler.BeginSample("Regular Cast");
        Ray ray = _query.GetWorldRay();
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, _query.MaxDistance, _query.LayerMask, _query.TriggerInteraction);
        Profiler.EndSample();
        if (hit)
        {
            Debug.Log($"Regular cast hit {hitInfo.collider}");
        }
        Physics.RaycastAll(_query.GetWorldRay());
    }
    private void Reset()
    {
        _query = GetComponent<PhysicsQuery>();
    }
}