using System.Collections.Generic;
using Data.Locations;

namespace Data.Encounters
{
    public interface IMap
    {
        public IList<ILocation> Locations { get; }
    }
}
