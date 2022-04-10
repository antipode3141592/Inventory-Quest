using Data.Shapes;
using System;
using System.Collections.Generic;

namespace Data.Items
{
    [Serializable]
    public class EquipableItemStats : IItemStats, IEquipableStats
    {
        //IItemStats
        public string Id { get; }
        public string Description { get; }
        public ShapeType ShapeType { get; }
        public Facing DefaultFacing { get; }

        public Rarity Rarity { get; }
        public float Weight { get; }
        public float GoldValue { get; }

        public string SpritePath { get; }

        public bool IsQuestItem { get; }

        //



        public List<StatModifier> Modifiers { get; set; }

        public EquipmentSlotType SlotType { get; set; }

        public EquipableItemStats(string itemId, float weight, float goldValue, string description, string spritePath, EquipmentSlotType slotType, ShapeType shapeType = ShapeType.Monomino, Facing defaultFacing = Facing.Right, StatModifier[] modifiers = null, Rarity rarity = Rarity.common, bool isQuest = false)
        {
            Id = itemId;
            ShapeType = shapeType;
            DefaultFacing = defaultFacing;
            Weight = weight;
            GoldValue = goldValue;
            Description = description;
            SpritePath = spritePath;
            Modifiers = modifiers != null ? new(modifiers) : new();
            SlotType = slotType;
            Rarity = rarity;
            IsQuestItem = isQuest;
        }
    }
}
