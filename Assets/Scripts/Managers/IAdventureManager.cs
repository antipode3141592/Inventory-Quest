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
        public ILocation DestinationLocation { get; }

        public event EventHandler OnAdventureCompleted;
        public event EventHandler OnAdventureStarted;
        public event EventHandler<AdventureStates> OnAdventureStateChanged;
        public event EventHandler OnEncounterListGenerated;
        public event EventHandler<string> OnCurrentLocationSet;
        public event EventHandler<string> OnDestinationLocationSet;

        public void StartAdventure();

        public void SetCurrentLocation(string id);
        public void SetDestinationLocation(string id);
    }
}