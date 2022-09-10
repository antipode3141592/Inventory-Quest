using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data.Locations
{
    [CreateAssetMenu(menuName = "InventoryQuest/LocationStats", fileName ="Location_")]
    public class LocationStatsSO : ScriptableObject, ILocationStats
    {
        [SerializeField] string id;
        [SerializeField] string displayName;
        [SerializeField] string thumbnailSpritePath;
        [SerializeField] string _name;
        [SerializeField] List<string> characterIds;
        [SerializeField] List<string> locationIds;
        [SerializeField] bool isKnown;

        public string Id => id;
        public string Name => _name;
        public string DisplayName => displayName;
        public string ThumbnailSpritePath => thumbnailSpritePath;
        public List<string> CharacterIds => characterIds;
        public List<string> LocationIds => locationIds;
        public bool IsKnown => isKnown;
    }
}
