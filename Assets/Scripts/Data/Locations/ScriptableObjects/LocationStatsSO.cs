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
        [SerializeField, PreviewField] Sprite thumbnailSprite;
        [FilePath(Extensions = ".unity"), SerializeField] string scenePath;
        [SerializeField] List<string> characterIds;
        [SerializeField] List<string> locationIds;
        [SerializeField] bool isKnown;
        [SerializeField] bool isTravelPoint;
        [InlineEditor(InlineEditorModes.FullEditor)]
        [SerializeField] AudioClip audioClip;

        public string Id => id;
        public string Name => _name;
        public string DisplayName => displayName;
        public Sprite ThumbnailSprite => thumbnailSprite;
        public string ScenePath => scenePath;
        public List<string> CharacterIds => characterIds;
        public List<string> LocationIds => locationIds;
        public bool IsKnown => isKnown;
        public bool IsTravelPoint => isTravelPoint;
        public AudioClip AudioClip => audioClip;
    }
}
