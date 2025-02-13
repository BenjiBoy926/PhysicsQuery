using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort3D : ResultSortGeneric<RaycastHit>
    {
        public static readonly ResultSort3D None = new ResultSort3D_None();
        public static readonly ResultSort3D Distance = new ResultSort3D_Distance();
    }
    public class ResultSort3D_None : ResultSort3D
    {
        protected override bool WillSort => false;
        protected override int Compare(RaycastHit a, RaycastHit b)
        {
            return 0;
        }
    }
    public class ResultSort3D_Distance : ResultSort3D
    {
        protected override int Compare(RaycastHit a, RaycastHit b)
        {
            return a.distance.CompareTo(b.distance);
        }
    }
}