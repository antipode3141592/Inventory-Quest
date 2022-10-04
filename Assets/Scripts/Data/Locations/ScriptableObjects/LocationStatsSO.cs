using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Locations
{
    [CreateAssetMenu(menuName = "InventoryQuest/LocationStats", fileName ="Location_")]
    public class LocationStatsSO : SerializedScriptableObject, ILocationStats
    {
        [SerializeField] string id;
        [SerializeField] string _name;
        [SerializeField] string displayName;
        [SerializeField] string thumbnailSpritePath;
        [FilePath(Extensions = ".unity"), SerializeField] string scenePath;
        [SerializeField] List<string> characterIds;
        [SerializeField] List<string> locationIds;
        [SerializeField] bool isKnown;

        public string Id => id;
        public string Name => _name;
        public string DisplayName => displayName;
        public string ThumbnailSpritePath => thumbnailSpritePath;
        public string ScenePath => scenePath;
        public List<string> CharacterIds => characterIds;
        public List<string> LocationIds => locationIds;
        public bool IsKnown => isKnown;
    }
}
