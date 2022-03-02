using Data.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data

{
    public class EquipableItem : IItem, IEquipable
    {
        public IItemStats Stats { get; }
        public Shape Shape { get; }

        public string GuId { get; }

        public string Id { get; }

        public EquipmentSlotType SlotType { get; }

        public float Weight => Stats.Weight;

        public float Value => Stats.GoldValue;

        public List<StatModifier> Modifiers { get ; set ; }
        public Sprite Sprite { get; set; }

        public EquipableItem(EquipableItemStats stats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = stats.Id;
            Stats = stats;
            Shape = ShapeFactory.GetShape(shape: stats.ShapeType, facing: stats.DefaultFacing);
            Modifiers = stats.Modifiers;
            SlotType = stats.SlotType;
            Sprite = Resources.Load<Sprite>(stats.SpritePath);
        }
    }
}
