using PQuery;
using UnityEngine;
public class CastTest : MonoBehaviour
{
    [SerializeField]
    private PhysicsQuery _query;
    private void Update()
    {
        _query.CastNonAlloc(ResultSort.Distance);
        _query.OverlapNonAlloc();
    }
    private void Reset()
    {
        _query = GetComponent<PhysicsQuery>();
    }
}