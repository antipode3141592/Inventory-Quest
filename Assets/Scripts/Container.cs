using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace InventoryQuest
{
    public class Container: IContainer
    {
        public string ItemId;
        public ItemStats MyStats;
        //shape
        public GridSquare[,] Grid;

        public Dictionary<string,Content> Contents;
        public Coor Size;

        public event EventHandler<GridEventArgs> OnGridChanged;
        public event EventHandler<GridEventArgs> OnGridHighlight;
        public event EventHandler<ContainerEventArgs> OnContainerChanged;

        public Container(ItemStats stats, Coor size)
        {
            MyStats = stats;
            Size = size;
            Grid = new GridSquare[size.row, size.column];
            Contents = new Dictionary<string, Content>();
            
        }

        public float TotalWeight => Contents.Sum(x => x.Value.Item.ItemStats.Weight) + MyStats.Weight;
        public float TotalWorth => Contents.Sum(x => x.Value.Item.ItemStats.GoldValue) + MyStats.GoldValue;
        public float ContainedWeight => Contents.Sum(x => x.Value.Item.ItemStats.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Value.Item.ItemStats.GoldValue);

        public bool TryPlace(Item item, Coor target)
        {
            if (IsPointInGrid(target) && !Grid[target.row, target.column].IsOccupied)
            {
                List<Coor> tempPointList = ListPool<Coor>.Get();
                for (int r = 0; r < item.ItemShape.Size.row; r++)   
                {
                    for (int c = 0; c < item.ItemShape.Size.column; c++)
                    {
                        Debug.Log($"Current Shape Facing :{item.ItemShape.CurrentFacing}, Size.rows: {item.ItemShape.Size.row}, Size.columns: {item.ItemShape.Size.column}");
                        Debug.Log($"Current Mask{item.ItemShape.CurrentMask}");
                        if (Grid[target.row + r, target.column + c].IsOccupied && item.ItemShape.CurrentMask.Map[r, c])
                        {
                            Debug.Log($"TryPlace() failed for {item.Name} at point [{target.row + r}, {target.column + c}]");
                            return false;
                        }
                        else if (item.ItemShape.CurrentMask.Map[r, c])
                        {
                            tempPointList.Add(new Coor(r: target.row + r, c: target.column + c));
                        }
                    }
                }
                //place item
                Contents.Add(item.Id, new Content(item, tempPointList));
                for (int i = 0; i < tempPointList.Count; i++)
                {
                    Grid[tempPointList[i].row, tempPointList[i].column].IsOccupied = true;
                    Grid[tempPointList[i].row, tempPointList[i].column].storedItemId = item.Id;
                }
                LogGrid();
                LogContents();
                return true;
            }
            Debug.Log($"TryPlace() failed for {item.Name} at point [{target.row},{target.column}]");
            return false;
        }

        private void LogContents()
        {
            Debug.Log($"Container now contains {Contents.Count} items:");
            foreach (var content in Contents)
            {
                Debug.Log($"....{content.Value.Item.Name}, {content.Value.Item.ItemStats.GoldValue}, {content.Value.Item.ItemStats.Weight},{content.Value.Item.Id}");
            }
            Debug.Log($"Total Combined Gold Value: {ContainedWorth}");
            Debug.Log($"Contained Weight: {ContainedWeight}");
            Debug.Log($"Total Combined Weight: {TotalWeight}");
        }

        private void LogGrid()
        {
            Debug.Log($"Container Grid:");
            for (int r = 0; r < Size.row; r++)
            {
                string line = "";
                for (int c = 0; c< Size.column; c++)
                {
                    line += Grid[r, c].IsOccupied ? "X" : "0";
                }
                Debug.Log($"....{r}: {line}");
            }
        }

        private void LogItemShape(Item item)
        {
            Debug.Log($"Item Shape:");
            for (int r = 0; r < item.ItemShape.Size.row; r++)
            {
                string line = "";
                for (int c = 0; c < item.ItemShape.Size.column; c++)
                {
                    line += item.ItemShape.CurrentMask.Map[r,c] ? "X" : "0";
                }
            }
        }

        bool IsPointInGrid (Coor target)
        {
            if (target.row <= Size.row && target.row >= 0 && target.column <= Size.column && target.column >=0)
            { 
                return true; 
            }
            return false;
        }

        public bool TryTake(out Item item, Coor target)
        {
            if (IsPointInGrid (target) && Grid[target.row, target.column].IsOccupied)
            {
                if (Contents.TryGetValue(key: Grid[target.row, target.column].storedItemId, out Content content))
                {
                    item = content.Item;
                    Debug.Log($"the item {item.Name} at {target.row},{target.column} is associated with these {content.GridSpaces.Count} grid spaces:");
                    Contents.Remove(key: Grid[target.row, target.column].storedItemId);
                    foreach (Coor coor in content.GridSpaces)
                    {
                        Debug.Log($"....{coor}");
                        Grid[coor.row, coor.column].IsOccupied = false;
                        Grid[coor.row, coor.column].storedItemId = "";
                    }
                    ListPool<Coor>.Release(content.GridSpaces);
                    
                    
                    OnContainerChanged?.Invoke(this, new ContainerEventArgs(this));
                    LogContents();
                    return true;
                }
            }
            item = null;
            return false;
        }
    }

    public struct GridSquare
    {
        public string storedItemId;
        public bool IsOccupied;
    }
    
}
