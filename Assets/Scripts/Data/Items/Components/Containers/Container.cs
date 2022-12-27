using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Data.Items
{
    public class Container: IContainer
    {
        public Container(IContainerStats stats, IItem parentItem)
        {
            Item = parentItem;
            GuId = Guid.NewGuid().ToString();
            Contents = new Dictionary<string, Content>();
            //initialize grid
            Grid = new Dictionary<Coor, GridSquare>();
            foreach(var point in stats.Grid)
            {
                Grid.Add(point, new GridSquare());
            }
        }

        public IItem Item { get; }
        public IDictionary<Coor, GridSquare> Grid { get; protected set; }
        public IDictionary<string, Content> Contents { get; protected set; }
        public string GuId { get; protected set; }

        public bool IsEmpty => ContainerIsEmpty();

        public bool IsFull => ContainerIsFull();

        public float InitialWeight => Item.Stats.Weight;
        public float Weight => InitialWeight + Contents.Sum(x => x.Value.Item.Weight);
        public float Value => Item.Stats.GoldValue;
        public float TotalWeight => Contents.Sum(x => x.Value.Item.Weight) + Weight;
        public float TotalWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue) + Value;
        public float ContainedWeight => Contents.Sum(x => x.Value.Item.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue);

        public event EventHandler<string> OnItemPlaced;
        public event EventHandler<string> OnItemTaken;
        public event EventHandler<HashSet<string>> OnMatchingItems;
        public event EventHandler<HashSet<string>> OnStackComplete;

        public bool IsPointInGrid(Coor target)
        {
            return Grid.ContainsKey(target);
        }

        public bool IsValidPlacement(IItem item, Coor target)
        {
            if (item.Shape is null)
            {
                if (Debug.isDebugBuild)
                    Debug.LogWarning($"no shape found for item {item.Id}");
                return false;
            }
            var currentItemPoints = item.Shape.Points[item.CurrentFacing];
            foreach (var point in currentItemPoints)
            {
                Coor testPoint = new(r: target.row + point.row, c: target.column + point.column);
                if (!Grid.ContainsKey(testPoint))
                    return false;
                if (Grid[testPoint].IsOccupied)
                {
                    if (!StackHasSpace(item, testPoint))
                        return false;
                }       
            }
            return true;
        }

        bool StackHasSpace(IItem item, Coor testPoint)
        {
            if (!item.Stats.IsStackable)
                return false;
            IItem _item = Contents[Grid[testPoint].storedItemGuId].Item;
            if (!String.Equals(_item.Id, item.Id, StringComparison.OrdinalIgnoreCase))
                return false;
            if (_item.Quantity >= _item.Stats.MaxQuantity)
                return false;
            return true;
        }

        public void GetPointHighlights(ref List<Tuple<HighlightState, Coor>> pointList, IItem item, Coor target)
        {
            if (item.Shape is null)
            {
                if (Debug.isDebugBuild)
                    Debug.LogWarning($"no shape found for item {item.Id}");
                return;
            }
            var currentItemPoints = item.Shape.Points[item.CurrentFacing];
            foreach (var point in currentItemPoints)
            {
                Coor testPoint = new(r: target.row + point.row, c: target.column + point.column);
                if (!Grid.ContainsKey(testPoint))
                {
                    pointList.Clear();
                    return;
                }
                if (Grid[testPoint].IsOccupied)
                    if (StackHasSpace(item, testPoint))
                        pointList.Add(new Tuple<HighlightState, Coor>(HighlightState.Highlight, testPoint));
                    else
                        pointList.Add(new Tuple<HighlightState, Coor>(HighlightState.Incorrect, testPoint));
                else
                    pointList.Add(new Tuple<HighlightState, Coor>(HighlightState.Highlight, testPoint));
            }
        }

        void AfterItemPlaced(IItem item)
        {
            //HashSet<string> _neighboors = HashSetPool<string>.Get(); // new();
            //if (MatchingNeighboors(item, _neighboors))
            //{
            //    if (_neighboors.Count >= stackable.MinStackSize)
            //    {
            //        OnStackComplete?.Invoke(this, _neighboors);
            //        CreateStack(_neighboors.ToList(), Contents[item.GuId].AnchorPosition, Contents[item.GuId].Item.Shape.CurrentFacing);
            //    }
            //    else
            //        OnMatchingItems?.Invoke(this, _neighboors);
            //}
            //HashSetPool<string>.Release(_neighboors);
        }

        //void CreateStack(List<string> items, Coor anchorPosition, Facing facing)
        //{
        //    StackableItemStats itemStats = Contents[items[0]].Item.Stats as StackableItemStats;
        //    if (itemStats is null)
        //        return;
        //    IItem item = ItemFactory.GetItem(itemStats);
        //    item.Shape.CurrentFacing = facing;
        //    int total = 0;
        //    foreach(var _item in items)
        //        total += Contents[_item].Item.Quantity;
        //    Debug.Log($"CreateStack:  qty {total}");
        //    item.Quantity = total;
        //    for (int i = 0; i < items.Count; i++)
        //        TryTake(item: out _, target: Contents[items[i]].GridSpaces[0]);
        //    TryPlace(item, anchorPosition);
        //    Debug.Log($"ItemQty: {item.Quantity}");
        //}

        public bool MatchingNeighboors(IItem item, HashSet<string> matchingNeighboors)
        {
            return false;
        }
        //public bool MatchingNeighboors(IItem item, HashSet<string> matchingNeighboors)
        //{
        //    IStackable stackable = item as IStackable;
        //    if (stackable is null)
        //        return false;
        //    matchingNeighboors.Add(item.GuId);
        //    int startingCount = matchingNeighboors.Count;
        //    Shape shape = item.Shape;
        //    Coor startingCoor = Contents[item.GuId].AnchorPosition;

        //    for (int r = 0; r < shape.Size.row; r++)
        //    {
        //        for (int c = 0; c < shape.Size.column; c++)
        //        {
        //            if (!shape.CurrentMask.Map[r, c])
        //                break;

        //            //check orthogonal only
        //            Check(r - 1 + startingCoor.row, c + startingCoor.column);
        //            Check(r + 1 + startingCoor.row, c + startingCoor.column);
        //            Check(r + startingCoor.row, c + 1 + startingCoor.column);
        //            Check(r + startingCoor.row, c - 1 + startingCoor.column);

        //            void Check(int r, int c)
        //            {
        //                if (!IsPointInGrid(new(r, c)))
        //                    return;
        //                if (!Grid[r, c].IsOccupied)
        //                    return;
        //                string storedGuId = Grid[r, c].storedItemId;
        //                string storedItemId = Contents[storedGuId].Item.Id;
        //                if (storedItemId != item.Id)
        //                    return;
        //                if (matchingNeighboors.Count > 0 && matchingNeighboors.Contains(storedGuId))
        //                    return;

        //                matchingNeighboors.Add(storedGuId);
        //            }
        //        }
        //    }

        //    if (matchingNeighboors.Count == startingCount)  //no adjacent 
        //        return false;

        //    HashSet<string> _addList = HashSetPool<string>.Get();// new();
        //    foreach (var _item in matchingNeighboors)
        //    {
        //        HashSet<string> _matchingNeighboors = new();
        //        foreach (var match in matchingNeighboors)
        //            _matchingNeighboors.Add(match);
        //        if (MatchingNeighboors(Contents[_item].Item, _matchingNeighboors))
        //            foreach (var __item in _matchingNeighboors)
        //                _addList.Add(__item);
        //    }
        //    foreach (var addition in _addList)
        //        matchingNeighboors.Add(addition);
        //    HashSetPool<string>.Release(_addList);
        //    return true;
        //}

        public bool TryPlace(IItem item, Coor target)
        {
            if (!IsValidPlacement(item, target))
                return false;

            var currentItemPoints = item.Shape.Points[item.CurrentFacing];
            // if it's stackable, increase quantity of item in target location by item quantity
            if (item.Stats.IsStackable)
            {

                IItem _item = null;
                //reduce item quantity by 
                if (!Grid.ContainsKey(target))
                    foreach (var point in currentItemPoints)
                    {
                        Coor testPoint = new(r: target.row + point.row, c: target.column + point.column);
                        if (Grid.ContainsKey(testPoint) 
                            && Grid[testPoint].IsOccupied
                            && Contents[Grid[testPoint].storedItemGuId].Item.Id == item.Id)
                        {
                            _item = Contents[Grid[testPoint].storedItemGuId].Item;
                            break;
                        }
                    }
                if (Grid[target].IsOccupied)
                    _item = Contents[Grid[target].storedItemGuId].Item;
                if (_item is not null)
                {
                    int qtyToAdd = Mathf.Clamp(item.Quantity, 0, _item.Stats.MaxQuantity - _item.Quantity);
                    _item.Quantity += qtyToAdd;
                    item.Quantity -= qtyToAdd;
                    OnItemPlaced?.Invoke(this, item.GuId);
                    return true;
                }
            }

            List<Coor> tempPointList = ListPool<Coor>.Get();
                
            foreach (var point in currentItemPoints)
            {
                Coor testPoint = new(r: target.row + point.row, c: target.column + point.column);
                tempPointList.Add(testPoint);
                Grid[testPoint].IsOccupied = true;
                Grid[testPoint].storedItemGuId = item.GuId;
            }

            item.RequestDestruction += RequestDestroyHandler;

            Contents.Add(item.GuId, new Content(item, tempPointList, target));
            OnItemPlaced?.Invoke(this, item.GuId);
            if (Debug.isDebugBuild)
                Debug.Log($"item {item.Id} x{item.Quantity} placed into container {Item.Id}");
            AfterItemPlaced(item);
            return true;
        }

        public bool TryTake(out IItem item, Coor target)
        {
            
            if (IsPointInGrid(target) && Grid[target].IsOccupied)
            {
                if (Contents.TryGetValue(key: Grid[target].storedItemGuId, out Content content))
                {
                    item = content.Item;
                    item.RequestDestruction -= RequestDestroyHandler;
                    Contents.Remove(key: Grid[target].storedItemGuId);
                    foreach (Coor coor in content.GridSpaces)
                    {
                        Grid[coor].IsOccupied = false;
                        Grid[coor].storedItemGuId = "";
                    }
                    ListPool<Coor>.Release(content.GridSpaces);
                    OnItemTaken?.Invoke(this, item.Id);
                    if (Debug.isDebugBuild)
                        Debug.Log($"TryTake item with guid: {item.GuId} and item id: {item.Id}"); 
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
                if (square.Value.IsOccupied) return false;
            }
            return true;
        }

        bool ContainerIsFull()
        {
            foreach (var square in Grid)
            {
                if (!square.Value.IsOccupied) return false;
            }
            return true;
        }

        public void RequestDestroyHandler(object sender, EventArgs e)
        {
            if (sender is not IItem item) return;
            if (!Contents.ContainsKey(item.GuId)) return;
            var anchor = Contents[item.GuId].AnchorPosition;
            TryTake(out _, anchor);
        }
    }
}