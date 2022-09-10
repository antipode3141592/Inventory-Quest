using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Locations
{
    public class LocationDataSourceScriptableObjectTest : SerializedMonoBehaviour, ILocationDataSource
    {
        [OdinSerialize]
        Dictionary<string, ILocationStats> locations;

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