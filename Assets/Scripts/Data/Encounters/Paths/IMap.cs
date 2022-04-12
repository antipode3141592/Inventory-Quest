using System.Collections.Generic;


namespace Data.Encounters
{
    public interface IMap
    {
        public IList<ILocation> Locations { get; }
    }
}
