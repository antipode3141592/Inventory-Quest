using System;

namespace Data
{
    public class GridEventArgs: EventArgs
    {
        public Coor[] GridPositions;
        public HighlightState State;

        public GridEventArgs(Coor[] gridPosition, HighlightState targetState)
        {
            GridPositions = gridPosition;
            State = targetState;
        }
    }

    
}
