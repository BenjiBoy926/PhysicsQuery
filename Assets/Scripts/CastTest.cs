using PQuery;
using UnityEngine;
public class CastTest : MonoBehaviour
{
    [SerializeField]
    private PhysicsQuery _query;
    private void Update()
    {
        _query.Cast(ResultSort.None);
    }
    private void Reset()
    {
        _query = GetComponent<PhysicsQuery>();
    }
}