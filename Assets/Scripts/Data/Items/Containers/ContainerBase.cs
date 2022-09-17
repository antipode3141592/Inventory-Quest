using Data.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Data.Items
{
    public abstract class ContainerBase: IItem, IContainer
    {
        public ContainerBase(IItemStats stats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = stats.Id;
            Stats = stats;
            Shape = ShapeFactory.GetShape(stats.ShapeType, stats.DefaultFacing);
            Contents = new Dictionary<string, Content>();
            Sprite = Resources.Load<Sprite>(stats.SpritePath);
            Quantity = 1;
        }

        public Coor Dimensions { get; protected set; }
        public GridSquare[,] Grid { get; protected set; }

        public string Id { get; protected set; }
        
        public IDictionary<string, Content> Contents { get; protected set; }
        public string GuId { get; protected set; }

        public Rarity Rarity { get; protected set; }
        public IItemStats Stats { get; protected set; }
        public Shape Shape { get; protected set; }
        public Sprite Sprite { get; set; }

        public int Quantity { get; }

        public bool IsEmpty => ContainerIsEmpty();

        public bool IsFull => ContainerIsFull();

        public float InitialWeight => Stats.Weight;
        public float Weight => InitialWeight + Contents.Sum(x => x.Value.Item.Weight);
        public float Value => Stats.GoldValue;
        public float TotalWeight => Contents.Sum(x => x.Value.Item.Weight) + Weight;
        public float TotalWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue) + Value;
        public float ContainedWeight => Contents.Sum(x => x.Value.Item.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue);

        public event EventHandler<GridEventArgs> OnItemPlaced;
        public event EventHandler<GridEventArgs> OnItemTaken;
        public event EventHandler<HashSet<string>> OnMatchingItems;
        public event EventHandler<HashSet<string>> OnStackComplete;

        public bool IsPointInGrid(Coor target)
        {
            if ((0 <= target.row && target.row < Dimensions.row)
                && (0 <= target.column && target.column < Dimensions.column))
            {
                return true;
            }
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
            catch (Exception)
            {
                //Debug.LogWarning($"{ex.Message}");
                return false;
            }
        }

        void AfterItemPlaced(IItem item)
        {
            IStackable stackable = item as IStackable;
            if (stackable is null)
                return;
            HashSet<string> _neighboors = HashSetPool<string>.Get(); // new();
            if (MatchingNeighboors(item, _neighboors))
            {
                if (_neighboors.Count >= stackable.MinStackSize)
                {
                    OnStackComplete?.Invoke(this, _neighboors);
                    CreateStack(_neighboors.ToList(), Contents[item.GuId].AnchorPosition, Contents[item.GuId].Item.Shape.CurrentFacing);
                }
                else
                    OnMatchingItems?.Invoke(this, _neighboors);
            }
            HashSetPool<string>.Release(_neighboors);
        }

        void CreateStack(List<string> items, Coor anchorPosition, Facing facing)
        {
            StackableItemStats itemStats = Contents[items[0]].Item.Stats as StackableItemStats;
            if (itemStats is null)
                return;
            StackableItem item = ItemFactory.GetItem(itemStats) as StackableItem;
            item.Shape.CurrentFacing = facing;
            int total = 0;
            foreach(var _item in items)
                total += Contents[_item].Item.Quantity;
            Debug.Log($"CreateStack:  qty {total}");
            item.Quantity = total;
            for (int i = 0; i < items.Count; i++)
                TryTake(item: out _, target: Contents[items[i]].GridSpaces[0]);
            TryPlace(item, anchorPosition);
            Debug.Log($"ItemQty: {item.Quantity}");
        }

        public bool MatchingNeighboors(IItem item, HashSet<string> matchingNeighboors)
        {
            IStackable stackable = item as IStackable;
            if (stackable is null)
                return false;
            matchingNeighboors.Add(item.GuId);
            int startingCount = matchingNeighboors.Count;
            Shape shape = item.Shape;
            Coor startingCoor = Contents[item.GuId].AnchorPosition;
            
            for (int r = 0; r < shape.Size.row; r++)
            {
                for (int c = 0; c < shape.Size.column; c++)
                {
                    if (!shape.CurrentMask.Map[r, c])
                        break;
                    
                    //check orthogonal only
                    Check(r - 1 + startingCoor.row, c + startingCoor.column);
                    Check(r + 1 + startingCoor.row, c + startingCoor.column);
                    Check(r + startingCoor.row, c + 1 + startingCoor.column);
                    Check(r + startingCoor.row, c - 1 + startingCoor.column);

                    void Check(int r, int c)
                    {
                        if (!IsPointInGrid(new(r, c)))
                            return;
                        if (!Grid[r, c].IsOccupied)
                            return;
                        string storedGuId = Grid[r, c].storedItemId;
                        string storedItemId = Contents[storedGuId].Item.Id;
                        if (storedItemId != item.Id)
                            return;
                        if (matchingNeighboors.Count > 0 && matchingNeighboors.Contains(storedGuId))
                            return;

                        matchingNeighboors.Add(storedGuId);
                    }
                }
            }

            if (matchingNeighboors.Count == startingCount)  //no adjacent 
                return false;

            HashSet<string> _addList = HashSetPool<string>.Get();// new();
            foreach (var _item in matchingNeighboors)
            {
                HashSet<string> _matchingNeighboors = new();
                foreach (var match in matchingNeighboors)
                    _matchingNeighboors.Add(match);
                if (MatchingNeighboors(Contents[_item].Item, _matchingNeighboors))
                    foreach (var __item in _matchingNeighboors)
                        _addList.Add(__item);
            }
            foreach (var addition in _addList)
                matchingNeighboors.Add(addition);
            HashSetPool<string>.Release(_addList);
            return true;
        }

        public bool TryPlace(IItem item, Coor target)
        {
            //if (Debug.isDebugBuild)
            //    Debug.Log($"TryPlace item with guid: {item.GuId}");
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

                OnItemPlaced?.Invoke(this, new GridEventArgs(tempPointList.ToArray(), HighlightState.Normal, target, item));
                
                AfterItemPlaced(item);
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
                    Contents.Remove(key: Grid[target.row, target.column].storedItemId);
                    foreach (Coor coor in content.GridSpaces)
                    {
                        Grid[coor.row, coor.column].IsOccupied = false;
                        Grid[coor.row, coor.column].storedItemId = "";
                    }
                    ListPool<Coor>.Release(content.GridSpaces);
                    OnItemTaken?.Invoke(this, new GridEventArgs(content.GridSpaces.ToArray(), HighlightState.Normal, target, item));
                    if (Debug.isDebugBuild)
                        Debug.Log($"TryTake item with guid: {item.GuId}");
                    return true;
                }
            }
            item = null;
            return false;
        }

        bool ContainerIsEmpty()
        {
            foreach (var square in Grid)
            {
                if (square.IsOccupied) return false;
            }
            return true;
        }

        bool ContainerIsFull()
        {
            foreach (var square in Grid)
            {
                if (!square.IsOccupied) return false;
            }
            return true;
        }
    }
}