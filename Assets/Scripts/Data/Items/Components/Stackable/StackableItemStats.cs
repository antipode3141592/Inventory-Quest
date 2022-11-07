using Data.Shapes;

namespace Data.Items

{
    public class StackableItemStats : IStackableStats
    {
        public StackableItemStats(int minStackSize)
        {
            MinStackSize = minStackSize;
        }

        public int MinStackSize { get; }
    }
}
