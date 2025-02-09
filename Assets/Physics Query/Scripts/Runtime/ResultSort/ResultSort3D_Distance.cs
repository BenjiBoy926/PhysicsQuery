using UnityEngine;

namespace PQuery
{
    internal class ResultSort3D_Distance : ResultSort3D
    {
        protected override int Compare(RaycastHit a, RaycastHit b)
        {
            return a.distance.CompareTo(b.distance);
        }
    }
}