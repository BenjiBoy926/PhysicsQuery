using UnityEngine;
using PQuery;

[ExecuteAlways]
public class ReportResults2D : MonoBehaviour
{
    [SerializeField]
    private PhysicsQuery2D _query;

    private void Update()
    {
        bool didHit = _query.Cast(out RaycastHit2D hit);
        if (didHit)
        {
            Debug.Log(hit.collider);
        }
    }

    private void Reset()
    {
        _query = GetComponent<PhysicsQuery2D>();
    }
}
