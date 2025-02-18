using UnityEngine;

namespace PQuery
{
    public static class ResultSort2D
    {
        public static readonly ResultSort<RaycastHit2D> None = new ResultSort2D_None();
        public static readonly ResultSort<RaycastHit2D> Distance = new ResultSort2D_Distance();
    }
    public class ResultSort2D_None : ResultSort_None<RaycastHit2D>
    {

    }
    public class ResultSort2D_Distance : ResultSort_Distance<RaycastHit2D>
    {
        protected override float Distance(RaycastHit2D hit)
        {
            return hit.distance;
        }
    }
}