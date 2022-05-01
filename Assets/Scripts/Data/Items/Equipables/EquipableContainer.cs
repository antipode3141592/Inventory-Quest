using Data.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Data.Items
{
    public class EquipableContainer : IItem, IEquipable, IContainer
    {
        public IItemStats Stats { get; }
        public Shape Shape { get; }

        public string GuId { get; }

        public string Id { get; }

        public EquipmentSlotType SlotType { get; }
        public GridSquare[,] Grid { get; }
        public IDictionary<string, Content> Contents { get; }

        public float Value => Stats.GoldValue;

        public IList<StatModifier> Modifiers { get; set; }
        public Sprite Sprite { get; set; }

        public float InitialWeight => Stats.Weight;
        public float TotalWeight => Contents.Sum(x => x.Value.Item.Stats.Weight) + Weight;
        public float TotalWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue) + Value;
        public float ContainedWeight => Contents.Sum(x => x.Value.Item.Stats.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue);
        public float Weight => InitialWeight + Contents.Sum(x => x.Value.Item.Stats.Weight);

        public event EventHandler<GridEventArgs> OnItemPlaced;
        public event EventHandler<GridEventArgs> OnItemTaken;

        public EquipableContainer(EquipableContainerStats stats)
        {
            GuId = Guid.NewGuid().ToString();
            Stats = stats;
            Shape = ShapeFactory.GetShape(stats.ShapeType, stats.DefaultFacing);

            Id = stats.Id;
            SlotType = stats.SlotType;
            Grid = new GridSquare[stats.ContainerSize.row, stats.ContainerSize.column]; ;
            Contents = new Dictionary<string, Content>();
            Modifiers = stats.Modifiers is not null ? stats.Modifiers : new List<StatModifier>();
            Sprite = Resources.Load<Sprite>(stats.SpritePath);
            Dimensions = stats.ContainerSize;
        }

        public bool TryPlace(IItem item, Coor target)
        {
            if (IsValidPlacement(item, target))
            {
                List<Coor> tempPointList = ListPool<Coor>.Get();
                for (int r = 0; r < item.Shape.Size.row; r++)
                {
                    for (int c = 0; c < item.Shape.Size.column; c++)
                    {
                        if (item.Shape.CurrentMask.Map[r, c])
                            tempPointList.Add(new Coor(r: target.row + r, c: target.column + c));
                    }
                }
                //place item
                Contents.Add(item.GuId, new Content(item, tempPointList, target));

                for (int i = 0; i < tempPointList.Count; i++)
                {
                    Grid[tempPointList[i].row, tempPointList[i].column].IsOccupied = true;
                    Grid[tempPointList[i].row, tempPointList[i].column].storedItemId = item.GuId;
                }
                //LogGrid();
                //LogContents();
                OnItemPlaced?.Invoke(this, new GridEventArgs(tempPointList.ToArray(), HighlightState.Normal, target, item));
                return true;
            }
            //Debug.Log($"TryPlace() failed for {item.Id} at point [{target}]");
            return false;
        }

        public bool IsPointInGrid(Coor target)
        {
            if (target.row <= Dimensions.row && target.row >= 0 && target.column <= Dimensions.column && target.column >= 0)
            {
                return true;
            }
            return false;
        }

        public bool TryTake(out IItem item, Coor target)
        {
            if (IsPointInGrid(target) && Grid[target.row, target.column].IsOccupied)
            {
                if (Contents.TryGetValue(key: Grid[target.row, target.column].storedItemId, out Content content))
                {
                    item = content.Item;
                    //Debug.Log($"the item {item.Id} at {target} is associated with these {content.GridSpaces.Count} grid spaces:");

                    Contents.Remove(key: Grid[target.row, target.column].storedItemId);
                    foreach (Coor coor in content.GridSpaces)
                    {
                        //Debug.Log($"....{coor.row},{coor.column}");
                        Grid[coor.row, coor.column].IsOccupied = false;
                        Grid[coor.row, coor.column].storedItemId = "";
                    }
                    ListPool<Coor>.Release(content.GridSpaces);
                    //LogContents();
                    //LogGrid();
                    OnItemTaken?.Invoke(this, new GridEventArgs(content.GridSpaces.ToArray(), HighlightState.Normal, target, item));
                    return true;
                }
            }
            item = null;
            return false;
        }

        public bool IsValidPlacement(IItem item, Coor target)
        {
            try
            {
                Shape shape = item.Shape;
                for (int r = 0; r < shape.Size.row; r++)
                {
                    for (int c = 0; c < shape.Size.column; c++)
                    {
                        int row = target.row + r;
                        int column = target.column + c;

                        if (!IsPointInGrid(new(row, column))) return false;
                        if (Grid[row, column].IsOccupied && shape.CurrentMask.Map[r, c]) return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"{ex.Message}");
                return false;
            }
        }

        public bool IsEmpty => isEmpty();

        public bool IsFull => isFull();

        public Coor Dimensions { get; }

        bool isEmpty()
        {
            foreach (var square in Grid)
            {
                if (square.IsOccupied) return false;
            }
            return true;
        }

        bool isFull()
        {
            foreach (var square in Grid)
            {
                if (!square.IsOccupied) return false;
            }
            return true;
        }
    }
}
