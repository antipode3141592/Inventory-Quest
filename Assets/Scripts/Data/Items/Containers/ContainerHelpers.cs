using Data.Shapes;
using System.Collections.Generic;

namespace Data.Items
{
    public static class ContainerHelpers
    {

        public static bool MatchingNeighboors(IItem item, IContainer container, ref HashSet<string> matchingNeighboors)
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

            if (matchingNeighboors.Count == 0)  //no adjacent 
                return false;

            HashSet<string> _matchingNeighboors = new();
            foreach (var _item in matchingNeighboors)
                if (MatchingNeighboors(container.Contents[_item].Item, container, ref _matchingNeighboors))
                    foreach (var __item in _matchingNeighboors)
                        matchingNeighboors.Add(__item);
            return true;
        }
    }
}