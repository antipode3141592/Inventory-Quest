using System.Collections.Generic;


namespace Data.Encounters
{
    public interface ILocation
    {
        public IList<IPath> Paths { get; }
    }
}
