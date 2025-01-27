namespace PQuery.Editor
{
    public readonly struct ElementIndex<TElement>
    {
        public readonly TElement Value;
        public readonly int Index;

        public ElementIndex(Result<TElement> result, int index)
        {
            Value = result[index];
            Index = index;
        }
    }
}