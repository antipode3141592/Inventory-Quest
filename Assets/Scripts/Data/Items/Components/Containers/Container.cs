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
            CanContainContainers = stats.CanContainContainers;
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

        public bool CanContainContainers { get; }

        public float InitialWeight => Item.Stats.Weight;
        public float Weight => InitialWeight + Contents.Sum(x => x.Value.Item.Weight);
        public float Value => Item.Stats.IndividualGoldValue;
        public float TotalWeight => Contents.Sum(x => x.Value.Item.Weight) + Weight;
        public float TotalWorth => Contents.Sum(x => x.Value.Item.Value) + Value;
        public float ContainedWeight => Contents.Sum(x => x.Value.Item.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Value.Item.Value);

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
            if (!CanContainContainers && item.Components.ContainsKey(typeof(IContainer)))
                return false;
            var currentItemPoints = item.Shape.Points[item.CurrentFacing];
            foreach (var point in currentItemPoints)
            {
                Coor testPoint = new(r: target.row + point.row, c: target.column + point.column);
                if (!IsValidPoint(item, testPoint))
                    return false;
            }
            return true;
        }

        bool IsValidPoint(IItem item, Coor target)
        {
            if (!Grid.ContainsKey(target))
                return false;
            if (Grid[target].IsOccupied)
            {
                if (!StackHasSpace(item, target))
                    return false;
            }
            return true;
        }

        bool StackHasSpace(IItem item, Coor testPoint)
        {
            if (!item.Stats.IsStackable)
                return false;
            if (!Contents.ContainsKey(Grid[testPoint].storedItemGuId))
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
            bool validPlacement = true;
            List<Coor> _pointList = ListPool<Coor>.Get();
            foreach (var point in item.Shape.Points[item.CurrentFacing])
            {
                Coor testPoint = new(r: target.row + point.row, c: target.column + point.column);
                if (!Grid.ContainsKey(testPoint))
                {
                    validPlacement = false;
                    continue;
                }
                validPlacement = 
                    validPlacement 
                    && IsValidPoint(item, testPoint) 
                    && !(!CanContainContainers && item.Components.ContainsKey(typeof(IContainer)));
                _pointList.Add(testPoint);
            }
            foreach(var point in _pointList)
                pointList.Add(new(validPlacement ? HighlightState.Highlight : HighlightState.Incorrect, point));
            ListPool<Coor>.Release(_pointList);

        }

        public bool MatchingNeighboors(IItem item, HashSet<string> matchingNeighboors)
        {
            return false;
        }

        public bool TryPlace(ref IItem item, Coor target)
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
                    if (item.Quantity <= 0)
                        item = null;
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
            item.QuantityChanged += QuanityChangedHandler;

            Contents.Add(item.GuId, new Content(item, tempPointList, target));
            OnItemPlaced?.Invoke(this, item.GuId);
            if (Debug.isDebugBuild)
                Debug.Log($"item {item.Id} x{item.Quantity} placed into container {Item.Id}");
            item = null;
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
                    item.QuantityChanged -= QuanityChangedHandler;
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
            var primaryPoint = Contents[item.GuId].GridSpaces[0];
            TryTake(out _, primaryPoint);
        }

        public void QuanityChangedHandler(object sender, EventArgs e)
        {
            OnItemPlaced?.Invoke(this, string.Empty);
        }
    }
}