using PQuery;
using UnityEngine;
public class CastTest : MonoBehaviour
{
    [SerializeField]
    private PhysicsQuery3D _query;
    private void Update()
    {
        _query.CastNonAlloc(ResultSort3D.Distance);
        _query.OverlapNonAlloc();
    }
    private void Reset()
    {
        _query = GetComponent<PhysicsQuery3D>();
    }
}