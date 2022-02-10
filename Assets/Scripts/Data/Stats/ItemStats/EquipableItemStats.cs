using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable]
    public class EquipableItemStats : IItemStats
    {
        public string Name { get; }
        public string Description { get; }
        public ShapeType ShapeType { get; }
        public Facing DefaultFacing { get; }
        public float Weight { get; }
        public float GoldValue { get; }

        public List<StatModifier> Modifiers { get; set; }

        public EquipableItemStats(string itemId, float weight, float goldValue, string description, ShapeType shape = ShapeType.Square1, Facing defaultFacing = Facing.Right, StatModifier[] modifiers = null)
        {
            Name = itemId;
            ShapeType = shape;
            DefaultFacing = defaultFacing;
            Weight = weight;
            GoldValue = goldValue;
            Description = description;
            Modifiers = modifiers != null ? new(modifiers) : new();
        }
    }
}
