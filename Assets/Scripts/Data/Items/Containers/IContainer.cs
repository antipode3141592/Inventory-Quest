using System;
using System.Collections.Generic;

namespace Data.Items
{
    public interface IContainer: IItemComponent
    {
        public float InitialWeight { get; }
        public float Weight { get; }
        public bool IsEmpty { get; }

        public bool IsFull { get; }

        public IDictionary<string, Content> Contents { get; }
        public IDictionary<Coor, GridSquare> Grid { get; }
        public string GuId { get; }

        public event EventHandler<string> OnItemPlaced;
        public event EventHandler<string> OnItemTaken;
        public event EventHandler<HashSet<string>> OnMatchingItems;
        public event EventHandler<HashSet<string>> OnStackComplete;

        public bool TryPlace(IItem item, Coor target);
        public bool TryTake(out IItem item, Coor target);

        public bool IsPointInGrid(Coor target);

        public bool IsValidPlacement(IItem item, Coor target);

        public bool MatchingNeighboors(IItem item, HashSet<string> matchingNeighboors);        
    }


}
