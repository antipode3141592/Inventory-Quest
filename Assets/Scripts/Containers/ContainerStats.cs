﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryQuest
{
    public class ContainerStats : IItemStats, IContainerStats
    {
        

        public string ItemId { get; }

        public ShapeType ShapeType { get; }

        public Facing DefaultFacing { get; }

        public float Weight { get; }

        public float GoldValue { get; }

        public Coor ContainerSize { get; }

        public string Description { get; }

        public ContainerStats(string itemId, float weight, float goldValue, string description, Coor containerSize, ShapeType shape = ShapeType.Square1, Facing defaultFacing = Facing.Right)
        {
            ItemId = itemId;
            Weight = weight;
            GoldValue = goldValue;
            Description = description;
            ContainerSize = containerSize;
            ShapeType = shape;
            DefaultFacing = defaultFacing;
        }
    }
}
