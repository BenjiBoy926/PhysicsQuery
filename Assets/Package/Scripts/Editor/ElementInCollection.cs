namespace PhysicsQuery.Editor
{
    public readonly struct ElementInCollection<TElement>
    {
        public readonly TElement Value;
        public readonly int Index;

        public ElementInCollection(Result<TElement> result, int index)
        {
            Value = result[index];
            Index = index;
        }
    }
}