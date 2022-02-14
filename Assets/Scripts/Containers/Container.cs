using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace InventoryQuest
{
    public class Container: IItem, IContainer
    {
        public string GuId { get; }
        public string Id { get; }
        public GridSquare[,] Grid;
        public Dictionary<string,Content> Contents;
        public Coor ContainerSize;
        public IItemStats Stats { get; }
        public Shape Shape { get; }

        public Container(ContainerStats stats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = stats.Id;
            ContainerSize = stats.ContainerSize;
            Stats = stats;
            Shape = ShapeFactory.GetShape(stats.ShapeType, stats.DefaultFacing);
            Grid = new GridSquare[stats.ContainerSize.row, stats.ContainerSize.column];
            Contents = new Dictionary<string, Content>();
        }

        public float TotalWeight => Contents.Sum(x => x.Value.Item.Stats.Weight) + Weight;
        public float TotalWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue) + Value;
        public float ContainedWeight => Contents.Sum(x => x.Value.Item.Stats.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue);
        public float Weight => Stats.Weight;
        public float Value => Stats.GoldValue;

        public event EventHandler<GridEventArgs> OnGridOccupied;
        public event EventHandler<GridEventArgs> OnGridUnoccupied;
        public event EventHandler<ContainerEventArgs> OnContainerChanged;

        public bool TryPlace(IItem item, Coor target)
        {
            if (IsValidPlacement(item, target))
            {
                List<Coor> tempPointList = ListPool<Coor>.Get();
                for (int r = 0; r < item.Shape.Size.row; r++)   
                {
                    for (int c = 0; c < item.Shape.Size.column; c++)
                    {
                        tempPointList.Add(new Coor(r: target.row + r, c: target.column + c));
                    }
                }
                //place item
                Contents.Add(item.GuId, new Content(item, tempPointList));
                OnGridOccupied?.Invoke(this, new GridEventArgs(tempPointList.ToArray(), HighlightState.Normal));
                for (int i = 0; i < tempPointList.Count; i++)
                {
                    Grid[tempPointList[i].row, tempPointList[i].column].IsOccupied = true;
                    Grid[tempPointList[i].row, tempPointList[i].column].storedItemId = item.GuId;
                }
                //LogGrid();
                //LogContents();
                return true;
            }
            Debug.Log($"TryPlace() failed for {item.Id} at point [{target}]");
            return false;
        }

        public bool IsPointInGrid(Coor target)
        {
            if (target.row <= ContainerSize.row && target.row >= 0 && target.column <= ContainerSize.column && target.column >= 0)
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
                    Debug.Log($"the item {item.Id} at {target} is associated with these {content.GridSpaces.Count} grid spaces:");
                    OnGridUnoccupied?.Invoke(this, new GridEventArgs(content.GridSpaces.ToArray(), HighlightState.Normal));
                    Contents.Remove(key: Grid[target.row, target.column].storedItemId);
                    foreach (Coor coor in content.GridSpaces)
                    {
                        Debug.Log($"....{coor.row},{coor.column}");
                        Grid[coor.row, coor.column].IsOccupied = false;
                        Grid[coor.row, coor.column].storedItemId = "";
                    }
                    ListPool<Coor>.Release(content.GridSpaces);


                    OnContainerChanged?.Invoke(this, new ContainerEventArgs(this));
                    //LogContents();
                    //LogGrid();
                    return true;
                }
            }
            item = null;
            return false;
        }

        public bool IsValidPlacement(IItem item, Coor target)
        {
            if (!IsPointInGrid(target)) return false;
            Shape shape = item.Shape;
            for (int r = 0; r < shape.Size.row; r++)
            {
                for (int c = 0; c < shape.Size.column; c++)
                {
                    int row = target.row + r;
                    int column = target.column + c;

                    if (!IsPointInGrid(new(row, column))) return false;
                    if(Grid[row, column].IsOccupied && shape.CurrentMask.Map[r, c]) return false;
                }
            }
            
            return true;
        }

        #region Logging
        private void LogContents()
        {
            Debug.Log($"Container now contains {Contents.Count} items:");
            foreach (var content in Contents)
            {
                Debug.Log($"....{content.Value.Item.Id}, {content.Value.Item.Value}g, {content.Value.Item.Weight}lbs");
            }
            Debug.Log($"Total Combined Gold Value: {ContainedWorth}");
            Debug.Log($"Contained Weight: {ContainedWeight}");
            Debug.Log($"Total Combined Weight: {TotalWeight}");
        }

        private void LogGrid()
        {
            Debug.Log($"Container Grid:");
            for (int r = 0; r < ContainerSize.row; r++)
            {
                string line = "";
                for (int c = 0; c< ContainerSize.column; c++)
                {
                    line += Grid[r, c].IsOccupied ? "X" : "0";
                }
                Debug.Log($"....{r}: {line}");
            }
        }

        private void LogItemShape(Item item)
        {
            Debug.Log($"Item Shape:");
            for (int r = 0; r < item.Shape.Size.row; r++)
            {
                string line = "";
                for (int c = 0; c < item.Shape.Size.column; c++)
                {
                    line += item.Shape.CurrentMask.Map[r,c] ? "X" : "0";
                }
            }
        }
        #endregion

        
    }
    
}