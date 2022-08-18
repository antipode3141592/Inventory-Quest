using Data.Encounters;
using Data.Locations;
using System;
using Zenject;

namespace Data
{
    public class GameStateDataSource : IGameStateDataSource
    {
        ILocationDataSource _locationDataSource;
        IPathDataSource _pathDataSource;

        public GameStateDataSource(ILocationDataSource locationDataSource, IPathDataSource pathDataSource)
        {
            _locationDataSource = locationDataSource;
            _pathDataSource = pathDataSource;
        }

        public IPath CurrentPath { get; protected set; }
        public ILocation DestinationLocation { get; protected set; }
        public ILocation CurrentLocation { get; protected set; }

        public event EventHandler<string> OnCurrentLocationSet;
        public event EventHandler<string> OnDestinationLocationSet;
        public event EventHandler<string> OnCurrentPathSet;

        public void SetCurrentLocation(string id)
        {
            CurrentLocation = LocationFactory.GetLocation(_locationDataSource.GetLocationById(id));
            OnCurrentLocationSet?.Invoke(this, id);
        }

        public void SetDestinationLocation(string id)
        {
            DestinationLocation = LocationFactory.GetLocation(_locationDataSource.GetLocationById(id));
            OnDestinationLocationSet?.Invoke(this, id);
        }

        public void SetCurrentPath()
        {
            var stats = _pathDataSource.GetPathForStartAndEndLocations(
                startLocationId: CurrentLocation.Stats.Id,
                endLocationId: DestinationLocation.Stats.Id);
            if (stats == null) return;
            CurrentPath = PathFactory.GetPath(stats);
            OnCurrentPathSet?.Invoke(this, stats.Id);
        }
    }
}
