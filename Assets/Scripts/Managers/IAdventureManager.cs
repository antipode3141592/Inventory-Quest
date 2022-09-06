using InventoryQuest.Managers.States;

namespace InventoryQuest.Managers
{
    public interface IAdventureManager
    {
        public Idle Idle { get; }
        public Pathfinding Pathfinding { get; }
        public Adventuring Adventuring { get; }
    }
}