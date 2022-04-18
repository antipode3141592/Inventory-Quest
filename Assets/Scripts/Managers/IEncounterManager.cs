using Data.Encounters;
using System;

namespace InventoryQuest.Managers
{
    public interface IEncounterManager
    {
        IEncounter CurrentEncounter { get; set; }

        public event EventHandler<string> OnEncounterLoaded;
        public event EventHandler<string> OnEncounterStart;
        public event EventHandler<string> OnEncounterResolveStart;
        public event EventHandler<string> OnEncounterResolveSuccess;
        public event EventHandler<string> OnEncounterResolveFailure;
        public event EventHandler<string> OnEncounterComplete;
        public event EventHandler<EncounterStates> OnEncounterStateChanged;

        void Continue();
    }
}