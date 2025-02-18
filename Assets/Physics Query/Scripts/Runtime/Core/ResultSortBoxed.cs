namespace PQuery
{
    public static class ResultSortBoxed
    {
        public static ResultSort<BoxedRaycastHit> None = new ResultSortBoxed_None();
        public static ResultSort<BoxedRaycastHit> Distance = new ResultSortBoxed_Distance();
    }
    public class ResultSortBoxed_None : ResultSort_None<BoxedRaycastHit>
    {

    }
    public class ResultSortBoxed_Distance : ResultSort_Distance<BoxedRaycastHit>
    {
        protected override float Distance(BoxedRaycastHit hit)
        {
            return hit.Distance;
        }
    }
}