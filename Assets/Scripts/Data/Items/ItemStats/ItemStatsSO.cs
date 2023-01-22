using Data.Shapes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items
{
    [CreateAssetMenu(menuName = "InventoryQuest/Item/Stats", fileName = "i_")]
    public class ItemStatsSO : SerializedScriptableObject, IItemStats
    {
        [SerializeField] string id;
        [SerializeField, TextArea(1,5)] string description;
        [SerializeField] ShapeSO shape;
        [SerializeField] Facing defaultFacing;
        [SerializeField] Rarity rarity;
        [SerializeField] float weight;
        [SerializeField] bool isStackable = false;
        [SerializeField] int maxQuantity = 1;
        [SerializeField] float individualGoldValue;
        [SerializeField, PreviewField] Sprite primarySprite;
        [SerializeField] bool isQuestItem;
        [OdinSerialize] IList<IItemComponentStats> components;
        [OdinSerialize] HashSet<Tag> tags;

        public string Id => id;
        public string Description => description;
        public IShape Shape => shape;
        public Facing DefaultFacing => defaultFacing;
        public Rarity Rarity => rarity;
        public float Weight => weight;
        public bool IsStackable => isStackable;
        public int MaxQuantity => maxQuantity;
        public float IndividualGoldValue => individualGoldValue;
        public Sprite PrimarySprite => primarySprite;
        public bool IsQuestItem => isQuestItem;
        public IList<IItemComponentStats> Components => components;
        public IEnumerable<Tag> Tags => tags;
    }
}
