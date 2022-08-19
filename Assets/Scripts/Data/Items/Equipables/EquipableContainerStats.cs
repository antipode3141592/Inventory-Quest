using Data.Shapes;
using System.Collections.Generic;

namespace Data.Items
{
    public class EquipableContainerStats : IContainerStats, IEquipableStats
    {
        public EquipableContainerStats(string id, string description, float weight, float goldValue, Coor containerSize, string spritePath, ShapeType shapeType, EquipmentSlotType slotType, Facing defaultFacing = Facing.Right, Rarity rarity = Rarity.common,  bool isQuestItem = false, List<StatModifier> modifiers = null )
        {
            Id = id;
            Description = description;
            ShapeType = shapeType;
            DefaultFacing = defaultFacing;
            Rarity = rarity;
            Weight = weight;
            GoldValue = goldValue;
            ContainerSize = containerSize;
            SpritePath = spritePath;
            IsQuestItem = isQuestItem;
            Modifiers = modifiers;
            SlotType = slotType;
        }

        public string Id { get; }

        public string Description { get; }

        public ShapeType ShapeType { get; }

        public Facing DefaultFacing { get; }

        public Rarity Rarity { get; }

        public float Weight { get; }

        public float GoldValue { get; }

        public Coor ContainerSize { get; }



        public string SpritePath { get; }

        public bool IsQuestItem { get; }

        public List<StatModifier> Modifiers { get; set; }

        public EquipmentSlotType SlotType { get; set; }

        public bool IsStackable => false;

        public int MaxStackSize => 1;
    }
}
