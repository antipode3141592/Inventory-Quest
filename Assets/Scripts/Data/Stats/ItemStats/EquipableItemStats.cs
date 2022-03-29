using Data.Interfaces;
using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class EquipableItemStats : IItemStats
    {
        public string Id { get; }
        public string Description { get; }
        public ShapeType ShapeType { get; }
        public Facing DefaultFacing { get; }

        public Rarity Rarity { get; }
        public float Weight { get; }
        public float GoldValue { get; }

        public string SpritePath { get; }

        public List<StatModifier> Modifiers { get; set; }

        public EquipmentSlotType SlotType { get; set; }

        public EquipableItemStats(string itemId, float weight, float goldValue, string description, string spritePath, EquipmentSlotType slotType, ShapeType shape = ShapeType.Square1, Facing defaultFacing = Facing.Right, StatModifier[] modifiers = null, Rarity rarity = Rarity.common)
        {
            Id = itemId;
            ShapeType = shape;
            DefaultFacing = defaultFacing;
            Weight = weight;
            GoldValue = goldValue;
            Description = description;
            SpritePath = spritePath;
            Modifiers = modifiers != null ? new(modifiers) : new();
            SlotType = slotType;
            Rarity = rarity;
        }
    }
}
