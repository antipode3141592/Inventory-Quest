using System.Collections.Generic;

namespace Data.Locations
{
    public interface ILocationDataSource: IDataSource<ILocationStats>
    {
        public IDictionary<string, ILocationStats> Locations { get; }
    }
}