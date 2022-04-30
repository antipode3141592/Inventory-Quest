using Data.Characters;
using Data.Encounters;
using System.Collections.Generic;


namespace Data.Locations
{
    public interface ILocation
    {
        public IList<ILocation> Locations { get; }
        public IList<ICharacter> Characters { get; }

        public ILocationStats Stats { get; }
    }
}
