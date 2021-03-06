using Data.Characters;
using System.Collections.Generic;

namespace Data.Locations
{
    public class Location : ILocation
    {


        public IList<ILocation> Locations { get; } = new List<ILocation>();

        public IList<ICharacter> Characters { get; } = new List<ICharacter>();

        public ILocationStats Stats { get; }

        public Location(ILocationStats stats)
        {
            Stats = stats;
        }


        public void InitializeLocation(ICharacterDataSource characterData, ILocationDataSource locationData)
        {
            foreach (var character in Stats.CharacterIds)
            {
                Characters.Add(CharacterFactory.GetCharacter(characterData.GetCharacterStats(character)));
            }
            foreach (var location in Stats.LocationIds)
            {
                Locations.Add(LocationFactory.GetLocation(locationData.GetLocationById(location)));
            }
        }
    }
}
