namespace PQuery
{
    public interface IWrapper<TWrapped>
    {
        TWrapped Unwrap();
    }
}