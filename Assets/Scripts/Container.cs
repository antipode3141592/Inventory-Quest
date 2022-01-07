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
        public bool[,] Grid;

        public List<Content> Contents;
        public Vector2Int Size;

        public Container(ItemStats stats, Vector2Int size)
        {
            MyStats = stats;
            Size = size;
            Grid = new bool[size.x, size.y];
            Contents = new List<Content>();
        }

        public float TotalWeight => Contents.Sum(x => x.Item.ItemStats.Weight) + MyStats.Weight;
        public float TotalWorth => Contents.Sum(x => x.Item.ItemStats.GoldValue) + MyStats.GoldValue;

        public float ContainedWeight => Contents.Sum(x => x.Item.ItemStats.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Item.ItemStats.GoldValue);

        public bool TryPlace(Item item, Vector2Int target)
        {
            if (IsPointInGrid(target) && !Grid[target.x, target.y])
            {
                //to automatically release List
                using (ListPool<Vector2Int>.Get(out List<Vector2Int> tempPointList))
                {
                    for (int x = 0; x < item.ItemShape.Size.x; x++)
                    {
                        for (int y = 0; y < item.ItemShape.Size.y; y++)
                        {
                            if (Grid[target.x + x, target.y + y] && item.ItemShape.CurrentMask.Map[x, y])
                            {
                                return false;
                            } else if (item.ItemShape.CurrentMask.Map[x, y])
                            {
                                tempPointList.Add(new Vector2Int(x: target.x + x, target.y + y));
                            }
                        }
                    }
                    //place item
                    Contents.Add(new Content(item, tempPointList));
                    for (int i = 0; i < tempPointList.Count; i++)
                    {
                        Grid[tempPointList[i].x, tempPointList[i].y] = true;
                    }
                    //invoke OnPlace Event, sending 
                }

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

        public Item Take(Item item)
        {
            return null;
        }
    }

    
}
