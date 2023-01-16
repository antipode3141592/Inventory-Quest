using InventoryQuest.Managers.States;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IEncounterManager
    {
        public string CurrentStateName { get; }

        public Queue<EncounterModifier> EncounterModifiers { get; }

        public EncounterManagerStart START { get; }
        public Idle Idle { get; }
        public Wayfairing Wayfairing { get; }
        public Loading Loading { get; }
        public Resolving Resolving { get; }
        public CleaningUp CleaningUp { get; }
        public bool GameBeginning { get; set; }
        public bool GameEnding { get; set; }

        public void AddEncounterModifier(EncounterModifier encounterModifier);
    }
}