using UnityEngine;

namespace PQuery
{
    public static class ResultSort3D
    {
        public static readonly ResultSort<RaycastHit> None = new ResultSort3D_None();
        public static readonly ResultSort<RaycastHit> Distance = new ResultSort3D_Distance();
    }
    public class ResultSort3D_None : ResultSort_None<RaycastHit>
    {

    }
    public class ResultSort3D_Distance : ResultSort_Distance<RaycastHit>
    {
        protected override float Distance(RaycastHit hit)
        {
            return hit.distance;
        }
    }
}