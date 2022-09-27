using Data.Characters;
using Data.Encounters;
using System.Collections.Generic;


namespace Data.Locations
{
    public interface ILocation
    {
        public List<ILocation> Locations { get; }
        public List<ICharacter> Characters { get; }

        public ILocationStats Stats { get; }

        public void InitializeLocation(ICharacterDataSource characterData, ILocationDataSource locationData);
    }
}
