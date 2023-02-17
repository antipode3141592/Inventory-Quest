using InventoryQuest.Managers.States;

namespace InventoryQuest.Managers
{
    public interface IAdventureManager
    {
        public AdventureManagerStart AdventureManagerStart { get; }
        public Pathfinding Pathfinding { get; }
        public Adventuring Adventuring { get; }
        public InLocation InLocation { get; }
    }
}