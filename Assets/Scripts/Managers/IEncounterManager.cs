using InventoryQuest.Managers.States;

namespace InventoryQuest.Managers
{
    public interface IEncounterManager
    {
        public string CurrentStateName { get; }

        public Idle Idle { get; }
        public Wayfairing Wayfairing { get; }
        public Loading Loading { get; }
        public ManagingInventory ManagingInventory { get; }
        public Resolving Resolving { get; }
        public CleaningUp CleaningUp { get; }
    }
}