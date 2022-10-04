using System;
using System.Collections.Generic;

namespace Data.Items
{
    public interface IContainer: IItemComponent
    {
        public float InitialWeight { get; }
        public bool IsEmpty { get; }

        public bool IsFull { get; }

        public IDictionary<string, Content> Contents { get; }
        public GridSquare[,] Grid { get; }
        public Coor Dimensions { get; }

        public event EventHandler<GridEventArgs> OnItemPlaced;
        public event EventHandler<GridEventArgs> OnItemTaken;
        public event EventHandler<HashSet<string>> OnMatchingItems;
        public event EventHandler<HashSet<string>> OnStackComplete;

        public bool TryPlace(IItem item, Coor target);
        public bool TryTake(out IItem item, Coor target);

        public bool IsPointInGrid(Coor target);

        public bool IsValidPlacement(IItem item, Coor target);

        public bool MatchingNeighboors(IItem item, HashSet<string> matchingNeighboors);        
    }


}
