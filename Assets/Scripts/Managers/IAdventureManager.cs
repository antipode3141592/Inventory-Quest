using InventoryQuest.Managers.States;

namespace InventoryQuest.Managers
{
    public interface IAdventureManager
    {

        public AdventureManagerStart AdventureManagerStart { get; }
        public Idle Idle { get; }
        public Pathfinding Pathfinding { get; }
        public Adventuring Adventuring { get; }
        public InLocation InLocation { get; }

        public bool GameEnding { get; set; }
    }
}