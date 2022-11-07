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
        [SerializeField] string description;
        [SerializeField] IShape shape;
        [SerializeField] Facing defaultFacing;
        [SerializeField] Rarity rarity;
        [SerializeField] float weight;
        [SerializeField] float goldValue;
        [SerializeField] string spritePath;
        [SerializeField] bool isQuestItem;
        [OdinSerialize] IList<IItemComponentStats> components;

        public string Id => id;
        public string Description => description;
        public IShape Shape => shape;
        public Facing DefaultFacing => defaultFacing;
        public Rarity Rarity => rarity;
        public float Weight => weight;
        public float GoldValue => goldValue;
        public string SpritePath => spritePath;
        public bool IsQuestItem => isQuestItem;
        public IList<IItemComponentStats> Components => components;
    }
}
