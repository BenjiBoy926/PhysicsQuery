namespace PhysicsQuery.Editor
{
    public readonly struct ElementInCollection<TElement>
    {
        public readonly TElement Value;
        public readonly int Index;

        public ElementInCollection(TElement element, int index)
        {
            Value = element; 
            Index = index; 
        }
    }
}