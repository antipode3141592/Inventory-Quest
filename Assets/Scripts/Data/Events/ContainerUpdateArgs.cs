using System;

namespace Data
{
    public class ContainerUpdateArgs : EventArgs {
        public Coor[] HighlightedSquares;
        public GridSquareState State;

        public ContainerUpdateArgs(Coor[] highlightedSquares, GridSquareState state)
        {
            HighlightedSquares = highlightedSquares;
            State = state;
        }
    }
}
