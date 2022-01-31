using System;

namespace Data
{
    public class GridEventArgs: EventArgs
    {
        public Coor[] GridPositions;
        public GridSquareState State;

        public GridEventArgs(Coor[] gridPosition, GridSquareState targetState)
        {
            GridPositions = gridPosition;
            State = targetState;
        }
    }

    
}
