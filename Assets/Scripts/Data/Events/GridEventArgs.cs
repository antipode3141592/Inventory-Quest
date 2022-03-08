using Data.Interfaces;
using System;

namespace Data
{
    public class GridEventArgs: EventArgs
    {
        public Coor[] GridPositions;
        public HighlightState State;
        public Coor AnchorPosition;
        public IItem Item;


        public GridEventArgs(Coor[] gridPosition, HighlightState targetState, Coor anchorPosition, IItem item)
        {
            GridPositions = gridPosition;
            State = targetState;
            AnchorPosition = anchorPosition;
            Item = item;
        }
    }

    
}
