using System.Collections.Generic;
using Data.Locations;

namespace Data.Encounters
{
    public interface IMap
    {
        public LinkedList<ILocation> Locations { get; }

        public void GenerateMap();
    }
}
