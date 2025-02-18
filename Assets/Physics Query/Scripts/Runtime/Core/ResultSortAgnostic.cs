namespace PQuery
{
    public static class ResultSortAgnostic
    {
        public static readonly ResultSort<AgnosticRaycastHit> None = new ResultSortMinimal_None();
        public static readonly ResultSort<AgnosticRaycastHit> Distance = new ResultSortMinimal_Distance();
    }
    public class ResultSortMinimal_None : ResultSort_None<AgnosticRaycastHit>
    {

    }
    public class ResultSortMinimal_Distance : ResultSort_Distance<AgnosticRaycastHit>
    {
        protected override float Distance(AgnosticRaycastHit hit)
        {
            return hit.Distance;
        }
    }
}