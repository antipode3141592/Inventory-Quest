using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

namespace Data.Locations
{
    public class LocationDataSourceSO : SerializedMonoBehaviour, ILocationDataSource
    {
        [OdinSerialize] Dictionary<string, ILocationStats> locations;

        public IDictionary<string, ILocationStats> Locations => locations;

        public ILocationStats GetById(string id)
        {
            if (!locations.ContainsKey(id)) return null;
            return locations[id];
        }

        public ILocationStats GetRandom()
        {
            throw new System.NotImplementedException();
        }
    }
}