using PhysicsQuery;
using UnityEngine;
using Query = PhysicsQuery.PhysicsQuery;

public class CastTest : MonoBehaviour
{
    [SerializeField]
    private Query _query;
    private void Update()
    {
        _query.Cast(ResultSort.None);
    }
    private void Reset()
    {
        _query = GetComponent<Query>();
    }
}