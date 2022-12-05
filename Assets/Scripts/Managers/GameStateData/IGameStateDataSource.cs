using Data.Encounters;
using Data.Locations;
using System;
using System.Collections.Generic;

namespace InventoryQuest
{
    public interface IGameStateDataSource
    {
        public IPathStats CurrentPathStats { get; }
        public ILocation DestinationLocation { get; }
        public ILocation CurrentLocation { get; }

        public IEncounter CurrentEncounter { get; }

        public ICollection<string> KnownLocations { get; }

        public int CurrentIndex { get; set; }

        public event EventHandler<string> OnCurrentEncounterSet;
        public event EventHandler<string> OnCurrentLocationSet;
        public event EventHandler<string> OnDestinationLocationSet;
        public event EventHandler<string> OnCurrentPathSet;

        public void SetCurrentLocation(string id);

        public void SetDestinationLocation(string id);

        public void SetCurrentPath();

        public void SetCurrentEncounter();
    }
}
