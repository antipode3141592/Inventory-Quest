using System;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IContainer
    {
        public float InitialWeight { get; }
        public bool IsEmpty { get; }

        public bool IsFull { get; }

        public IDictionary<string, Content> Contents { get; }
        public GridSquare[,] Grid { get; }
        public Coor Dimensions { get; }

        public event EventHandler<GridEventArgs> OnItemPlaced;
        public event EventHandler<GridEventArgs> OnItemTaken;


        public bool TryPlace(IItem item, Coor target);
        public bool TryTake(out IItem item, Coor target);

        public bool IsValidPlacement(IItem item, Coor target);
        }


}
