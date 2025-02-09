using UnityEngine;

namespace PQuery
{
    internal class ResultSort3D_None : ResultSort3D
    {
        protected override int Compare(RaycastHit a, RaycastHit b)
        {
            return 0;
        }
    }
}