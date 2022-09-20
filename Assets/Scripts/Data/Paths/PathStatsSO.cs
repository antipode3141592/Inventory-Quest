using Data.Locations;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    [CreateAssetMenu(menuName = "InventoryQuest/PathStats", fileName = "Path_")]
    public class PathStatsSO : ScriptableObject, IPathStats
    {
        [SerializeField] string id;
        [SerializeField] string _name;
        [SerializeField] LocationStatsSO startLocationStats;
        [SerializeField] LocationStatsSO endLocationStats;
        [SerializeField] List<string> encounterIds;

        public string Id => id;
        public string Name => _name;
        public string StartLocationId => startLocationStats.Id;
        public string EndLocationId => endLocationStats.Id;
        public List<string> EncounterIds => encounterIds;
    }
}