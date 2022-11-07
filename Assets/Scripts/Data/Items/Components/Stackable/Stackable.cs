namespace Data.Items
{
    public class Stackable : IStackable
    {
        public Stackable(IStackableStats stats, IItem parentItem)
        {
            MinStackSize = stats.MinStackSize;
            Item = parentItem;
        }

        public int MinStackSize { get; }

        public IItem Item { get; }
    }
}
