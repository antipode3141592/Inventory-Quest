using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Vector2Int Size;

        public event EventHandler<GridEventArgs> OnGridChanged;
        public event EventHandler<GridEventArgs> OnGridHighlight;
        public event EventHandler<ContainerEventArgs> OnContainerChanged;

        public Container(ItemStats stats, Vector2Int size)
        {
            MyStats = stats;
            Size = size;
            Grid = new GridSquare[size.x, size.y];
            Contents = new Dictionary<string, Content>();
            
        }

        public float TotalWeight => Contents.Sum(x => x.Value.Item.ItemStats.Weight) + MyStats.Weight;
        public float TotalWorth => Contents.Sum(x => x.Value.Item.ItemStats.GoldValue) + MyStats.GoldValue;

        public float ContainedWeight => Contents.Sum(x => x.Value.Item.ItemStats.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Value.Item.ItemStats.GoldValue);

        public bool TryPlace(Item item, Vector2Int target)
        {
            if (IsPointInGrid(target) && !Grid[target.x, target.y].IsOccupied)
            {
                //using (ListPool<Vector2Int>.Get(out List<Vector2Int> tempPointList))
                //{
                List<Vector2Int> tempPointList = ListPool<Vector2Int>.Get();
                    for (int x = 0; x < item.ItemShape.Size.x; x++)
                    {
                        for (int y = 0; y < item.ItemShape.Size.y; y++)
                        {
                            if (Grid[target.x + x, target.y + y].IsOccupied && item.ItemShape.CurrentMask.Map[x, y])
                            {
                                return false;
                            }
                            else if (item.ItemShape.CurrentMask.Map[x, y])
                            {
                                tempPointList.Add(new Vector2Int(x: target.x + x, target.y + y));
                            }
                        }
                    }
                    //place item
                    Contents.Add(item.Id, new Content(item, tempPointList));
                    for (int i = 0; i < tempPointList.Count; i++)
                    {
                        Grid[tempPointList[i].x, tempPointList[i].y].IsOccupied = true;
                        Grid[tempPointList[i].x, tempPointList[i].y].storedItemId = item.Id;
                    }
                    //invoke OnPlace Event, sending 
                    //OnGridChanged?.Invoke(this, new GridEventArgs()
                //}
                LogContents();
                return true;
            }
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
            LogGrid();
        }

        private void LogGrid()
        {
            Debug.Log($"Container Grid:");
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                string line = "";
                for (int x = 0; x< Grid.GetLength(0); x++)
                {
                    line += Grid[x, y].IsOccupied ? "X" : "0";
                }
                Debug.Log($"....{y}: {line}");
            }
        }

        bool IsPointInGrid (Vector2Int target)
        {
            if (target.x <= Size.x && target.x >= 0 && target.y <= Size.y && target.y >=0)
            { 
                return true; 
            }
            return false;
        }

        public bool TryTake(out Item item, Vector2Int target)
        {
            if (IsPointInGrid (target) && Grid[target.x, target.y].IsOccupied)
            {
                if (Contents.TryGetValue(key: Grid[target.x, target.y].storedItemId, out Content content))
                {
                    item = content.Item;
                    Debug.Log($"the item {item.Name} at {target.x},{target.y} is associated with these {content.GridSpaces.Count} grid spaces:");
                    Contents.Remove(key: Grid[target.x, target.y].storedItemId);
                    foreach (Vector2Int coor in content.GridSpaces)
                    {
                        Debug.Log($"....{coor}");
                        Grid[coor.x, coor.y].IsOccupied = false;
                        Grid[coor.x, coor.y].storedItemId = "";
                    }
                    ListPool<Vector2Int>.Release(content.GridSpaces);
                    
                    
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
