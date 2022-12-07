using InventoryQuest.Managers.States;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IEncounterManager
    {
        public string CurrentStateName { get; }

        public List<EncounterModifier> EncounterModifiers { get; }

        public Idle Idle { get; }
        public Wayfairing Wayfairing { get; }
        public Loading Loading { get; }
        public ManagingInventory ManagingInventory { get; }
        public Resolving Resolving { get; }
        public CleaningUp CleaningUp { get; }
    }
}