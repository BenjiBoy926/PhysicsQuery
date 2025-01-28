using UnityEngine;

namespace PQuery
{
    internal class ResultSort_None : ResultSort
    {
        protected override int Compare(RaycastHit a, RaycastHit b)
        {
            return 0;
        }
    }
}