using UnityEngine;

namespace PQuery
{
    public class ResultSort2D_None : ResultSort2D
    {
        protected override bool WillSort => false;
        protected override int Compare(RaycastHit2D a, RaycastHit2D b)
        {
            return 0;
        }
    }
}