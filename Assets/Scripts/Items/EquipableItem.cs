using InventoryQuest;
using System;
using System.Collections.Generic;

namespace Data

{
    public class EquipableItem : IItem, IEquipable
    {
        public IItemStats Stats { get; }
        public Shape Shape { get; }

        public string Id { get; }

        public string Name { get; }

        public EquipmentSlotType SlotType { get; }

        public float Weight => Stats.Weight;

        public float Value => Stats.GoldValue;

        public List<StatModifier> Modifiers { get ; set ; }

        public EquipableItem(EquipableItemStats stats)
        {
            Id = Guid.NewGuid().ToString();
            Name = stats.ItemId;
            Stats = stats;
            Shape = ShapeFactory.GetShape(stats.ShapeType);
            Modifiers = stats.Modifiers;
        }
    }
}
