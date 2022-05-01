using Data.Encounters;
using Data.Locations;
using System;

namespace InventoryQuest.Managers
{
    public interface IAdventureManager
    {
        public IPath CurrentPath { get; }
        public AdventureStates CurrentState { get; }
        public ILocation CurrentLocation { get; }
        public ILocation EndLocation { get; }
        public ILocation StartLocation { get; }

        public event EventHandler OnAdventureCompleted;
        public event EventHandler OnAdventureStarted;
        public event EventHandler<AdventureStates> OnAdventureStateChanged;
        public event EventHandler OnEncounterListGenerated;

        public void ChoosePath(string pathId);
    }
}