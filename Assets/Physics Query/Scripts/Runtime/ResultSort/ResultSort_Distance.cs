using UnityEngine;

namespace PQuery
{
    internal class ResultSort_Distance : ResultSort
    {
        protected override int Compare(RaycastHit a, RaycastHit b)
        {
            return a.distance.CompareTo(b.distance);
        }
    }
}