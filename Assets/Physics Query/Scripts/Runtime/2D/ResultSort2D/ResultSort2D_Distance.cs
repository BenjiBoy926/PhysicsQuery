using UnityEngine;

namespace PQuery
{
    public class ResultSort2D_Distance : ResultSort2D
    {
        protected override int Compare(RaycastHit2D a, RaycastHit2D b)
        {
            return a.distance.CompareTo(b.distance);
        }
    }
}