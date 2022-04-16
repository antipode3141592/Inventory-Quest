using Data.Encounters;
using System;

namespace InventoryQuest.Managers
{
    public interface IAdventureManager
    {
        IPath CurrentPath { get; set; }
        AdventureStates CurrentState { get; set; }
        ILocation EndLocation { get; set; }
        ILocation StartLocation { get; set; }

        event EventHandler OnAdventureCompleted;
        event EventHandler OnAdventureStarted;
        event EventHandler<AdventureStates> OnAdventureStateChanged;
        event EventHandler OnEncounterListGenerated;

        void ChoosePath(string pathId);
    }
}