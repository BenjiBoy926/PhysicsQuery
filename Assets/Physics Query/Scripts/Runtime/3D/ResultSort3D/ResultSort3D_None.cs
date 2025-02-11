using UnityEngine;

namespace PQuery
{
    internal class ResultSort3D_None : ResultSort3D
    {
        protected override bool WillSort => false;
        protected override int Compare(RaycastHit a, RaycastHit b)
        {
            return 0;
        }
    }
}