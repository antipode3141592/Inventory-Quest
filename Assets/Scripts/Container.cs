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
                //to automatically release List
                using (ListPool<Vector2Int>.Get(out List<Vector2Int> tempPointList))
                {
                    for (int x = 0; x < item.ItemShape.Size.x; x++)
                    {
                        for (int y = 0; y < item.ItemShape.Size.y; y++)
                        {
                            if (Grid[target.x + x, target.y + y].IsOccupied && item.ItemShape.CurrentMask.Map[x, y])
                            {
                                return false;
                            } else if (item.ItemShape.CurrentMask.Map[x, y])
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
                }
                Debug.Log($"Container now contains:");
                foreach(var content in Contents)
                {
                    Debug.Log($"    {content.Value.Item.Name}, {content.Value.Item.ItemStats.GoldValue}, {content.Value.Item.ItemStats.Weight},{content.Value.Item.Id}");
                }
                Debug.Log($"Container Total Value: {ContainedWorth}");
                Debug.Log($"Container Total Weight: {TotalWeight}");
                return true;
            }
            return false;
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
