using Data.Shapes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items
{
    public class WoodHarvestSaw: IItem
    {
        readonly Coor wastePileAnchorPoint = new Coor(0, 10);
        IItemStats cuttingStats;


        public string GuId { get; }
        public string Id { get; }
        public IShape Shape { get; }
        public Facing CurrentFacing { get; protected set; }
        public Sprite Sprite { get; set; }
        public Rarity Rarity { get; }
        public float Weight => CalculateWeight();
        public float Value => Stats.GoldValue;
        public int Quantity { get; }
        public IDictionary<Type, IItemComponent> Components { get; }
        public IItemStats Stats { get; }

        public WoodHarvestSaw(IItemStats itemStats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = itemStats.Id;
            Stats = itemStats;
            Shape = itemStats.Shape;
            CurrentFacing = itemStats.DefaultFacing;
            Sprite = itemStats.PrimarySprite;
            Quantity = 1;
            Components = new Dictionary<Type, IItemComponent>();
        }

        public void Rotate(int direction)
        {
            int v = (int)CurrentFacing + direction;

            CurrentFacing = v % Shape.Points.Count < 0 ?
                (Facing)(Shape.Points.Count - 1) : (Facing)(v % Shape.Points.Count);
        }

        protected float CalculateWeight()
        {
            if (!Components.ContainsKey(typeof(IContainer)))
                return Stats.Weight;
            return (Components[typeof(IContainer)] as IContainer).Weight;
        }

        IContainer CuttingContainer => Components[typeof(IContainer)] as IContainer;

        public WoodHarvestSaw SubscribeToContainerEvents()
        {
            CuttingContainer.OnItemPlaced += OnItemPlacedHandler;
            return this;
        }

        public WoodHarvestSaw SetCutItem(IItemStats cuttingStats)
        {
            this.cuttingStats = cuttingStats;
            return this;
        }

        void OnItemPlacedHandler(object sender, string itemId)
        {
            if (itemId != "log_standard") return;

            CuttingContainer.TryTake(out _, new Coor(0, 0));

            var halfLog1 = ItemFactory.GetItem(cuttingStats);
            var halfLog2 = ItemFactory.GetItem(cuttingStats);

            CuttingContainer.TryPlace(halfLog1, new Coor(0, 0));
            CuttingContainer.TryPlace(halfLog2, wastePileAnchorPoint);
        }
    }
}
