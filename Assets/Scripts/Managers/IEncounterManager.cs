using Data.Encounters;
using System;

namespace InventoryQuest.Managers
{
    public interface IEncounterManager
    {
        IEncounter CurrentEncounter { get; set; }

        public event EventHandler OnEncounterLoaded;
        public event EventHandler OnEncounterStart;
        public event EventHandler OnEncounterResolveStart;
        public event EventHandler OnEncounterResolveSuccess;
        public event EventHandler OnEncounterResolveFailure;
        public event EventHandler OnEncounterComplete;
        public event EventHandler<EncounterStates> OnEncounterStateChanged;

        void Continue();
    }
}