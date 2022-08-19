using Data.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Data.Items
{
    public class ContainerBase: IItem, IContainer
    {
        public ContainerBase(IContainerStats stats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = stats.Id;
            Stats = stats;
            Shape = ShapeFactory.GetShape(stats.ShapeType, stats.DefaultFacing);
            
            Contents = new Dictionary<string, Content>();
            Sprite = Resources.Load<Sprite>(stats.SpritePath);
            Grid = new GridSquare[stats.ContainerSize.row, stats.ContainerSize.column];
            Dimensions = stats.ContainerSize;
        }

        public Coor Dimensions { get; }
        public GridSquare[,] Grid { get; }

        public string Id { get; }
        
        public IDictionary<string, Content> Contents { get; }
        public string GuId { get; }

        public Rarity Rarity { get; }
        public IItemStats Stats { get; }
        public Shape Shape { get; }
        public Sprite Sprite { get; set; }

        public bool IsEmpty => ContainerIsEmpty();

        public bool IsFull => ContainerIsFull();

        public float InitialWeight => Stats.Weight;
        public float Weight => InitialWeight + Contents.Sum(x => x.Value.Item.Stats.Weight);
        public float Value => Stats.GoldValue;
        public float TotalWeight => Contents.Sum(x => x.Value.Item.Stats.Weight) + Weight;
        public float TotalWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue) + Value;
        public float ContainedWeight => Contents.Sum(x => x.Value.Item.Stats.Weight);
        public float ContainedWorth => Contents.Sum(x => x.Value.Item.Stats.GoldValue);

        public event EventHandler<GridEventArgs> OnItemPlaced;
        public event EventHandler<GridEventArgs> OnItemTaken;

        public bool IsPointInGrid(Coor target)
        {
            if ((0 <= target.row && target.row <= Dimensions.row)
                && (0 <= target.column && target.column <= Dimensions.column))
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

        public bool MatchingNeighboors(IItem item, IContainer container, ref HashSet<string> matchingNeighboors)
        {
            int startingCount = matchingNeighboors.Count;
            Coor startingCoor = container.Contents[item.GuId].AnchorPosition;
            Shape shape = item.Shape;
            for (int r = 0; r < shape.Size.row; r++)
            {
                for (int c = 0; c < shape.Size.column; c++)
                {
                    if (!shape.CurrentMask.Map[r, c])
                        break;
                    //
                    for (int r1 = -1; r1 < 2; r1++)
                    {
                        for (int c1 = -1; c1 < 2; c1++)
                        {
                            int row = startingCoor.row + r + r1;
                            int column = startingCoor.column + c + c1;

                            if (!container.IsPointInGrid(new(row, column)))
                                break;
                            if (!container.Grid[row, column].IsOccupied)
                                break;
                            string storedItemId = container.Grid[row, column].storedItemId;
                            string storedGuId = container.Contents[storedItemId].Item.GuId;
                            if (storedGuId == item.GuId)
                                break;
                            if (storedItemId == item.Id)
                                break;
                            if (matchingNeighboors.Count > 0 && matchingNeighboors.Contains(storedItemId))
                                break;

                            matchingNeighboors.Add(storedGuId);
                        }
                    }
                }
            }

            if (matchingNeighboors.Count == startingCount)  //no adjacent 
                return false;

            HashSet<string> addList = new();
            foreach (var _item in matchingNeighboors)
            {
                Debug.Log($"match found for {item.Id}, {item.GuId}: {container.Contents[_item].Item.Id}, {_item}");
                HashSet<string> _matchingNeighboors = new();
                _matchingNeighboors.Add(item.GuId);
                if (MatchingNeighboors(container.Contents[_item].Item, container, ref _matchingNeighboors))
                    foreach (var __item in _matchingNeighboors)
                        addList.Add(__item);
            }
            foreach (var addition in addList)
                matchingNeighboors.Add(addition);

            return true;
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
                OnItemPlaced?.Invoke(this, new GridEventArgs(tempPointList.ToArray(), HighlightState.Normal, target, item));
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