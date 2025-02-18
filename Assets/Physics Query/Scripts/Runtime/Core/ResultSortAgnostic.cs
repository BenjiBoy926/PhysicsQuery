namespace PQuery
{
    public static class ResultSortAgnostic
    {
        public static readonly ResultSort<AgnosticRaycastHit> None = new ResultSortAgnostic_None();
        public static readonly ResultSort<AgnosticRaycastHit> Distance = new ResultSortAgnostic_Distance();
    }
    public class ResultSortAgnostic_None : ResultSort_None<AgnosticRaycastHit>
    {

    }
    public class ResultSortAgnostic_Distance : ResultSort_Distance<AgnosticRaycastHit>
    {
        protected override float Distance(AgnosticRaycastHit hit)
        {
            return hit.Distance;
        }
    }
}