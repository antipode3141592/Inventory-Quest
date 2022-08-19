using Data.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Data.Items
{
    public class EquipableContainer : ContainerBase, IEquipable
    {

        public EquipmentSlotType SlotType { get; }

        public IList<StatModifier> Modifiers { get; set; }

        public EquipableContainer(EquipableContainerStats stats) : base(stats)
        {

            SlotType = stats.SlotType;
            Modifiers = stats.Modifiers is not null ? stats.Modifiers : new List<StatModifier>();
            Grid = new GridSquare[stats.ContainerSize.row, stats.ContainerSize.column];
            Dimensions = stats.ContainerSize;
        }
    }
}
