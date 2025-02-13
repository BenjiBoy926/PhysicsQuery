using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort2D : ResultSortGeneric<RaycastHit2D>
    {
        public static readonly ResultSort2D None = new ResultSort2D_None();
        public static readonly ResultSort2D Distance = new ResultSort2D_Distance();
    }
    public class ResultSort2D_None : ResultSort2D
    {
        protected override bool WillSort => false;
        protected override int Compare(RaycastHit2D a, RaycastHit2D b)
        {
            return 0;
        }
    }
    public class ResultSort2D_Distance : ResultSort2D
    {
        protected override int Compare(RaycastHit2D a, RaycastHit2D b)
        {
            return a.distance.CompareTo(b.distance);
        }
    }
}