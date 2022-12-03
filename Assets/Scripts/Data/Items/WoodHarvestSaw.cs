using Data.Shapes;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items
{
    public class WoodHarvestSaw: IItem
    {
        readonly Coor wastePileAnchorPoint = new Coor(0, 10);

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

        public event EventHandler Cutting;

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

        Dictionary<int, IItemStats> cutItemStats;

        public WoodHarvestSaw SetCutItemDictionary(IList<IItemStats> cutItemList)
        {
            Debug.Log($"setting cut item dictionary...");
            if (cutItemStats is null) cutItemStats = new();
            for(int i = 0; i < cutItemList.Count; i++)
            {
                int key = int.Parse(cutItemList[i].Id.Split('_')[^1]);
                Debug.Log($"key: {key}, item id: {cutItemList[i]}");
                cutItemStats.Add(key, cutItemList[i]);
            }
            return this;
        }

        bool cutting = false;

        void OnItemPlacedHandler(object sender, string itemGuId)
        {
            if (cutting) return;
            cutting = true;
            var content = CuttingContainer.Contents[itemGuId];
            var anchor = content.AnchorPosition;

            int keepSideLength = wastePileAnchorPoint.column - anchor.column;
            if (keepSideLength <= 0)
            {
                cutting = false;
                return;
            }
            HashSet<Coor> itemPoints = content.Item.Shape.Points[content.Item.CurrentFacing];
            int wasteSideLength = itemPoints.Max(x => x.column) - keepSideLength + 1; //length inclusive of anchor

            if (wasteSideLength <= 0)
            {
                cutting = false;
                return;
            }
            if (Debug.isDebugBuild)
            {
                Debug.Log($"item {content.Item.Id} at {anchor}: ");
                Debug.Log($"    keep length {keepSideLength}, waste length {wasteSideLength}");
            }

            CuttingContainer.TryTake(out _, anchor);

            Cutting?.Invoke(this, EventArgs.Empty);

            var keepItem = ItemFactory.GetItem(cutItemStats[keepSideLength]);
            var wasteItem = ItemFactory.GetItem(cutItemStats[wasteSideLength]);

            CuttingContainer.TryPlace(keepItem, anchor);
            CuttingContainer.TryPlace(wasteItem, wastePileAnchorPoint);

            cutting = false;
        }
    }
}
