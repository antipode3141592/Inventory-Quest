using System;

namespace Data.Items
{
    public class ContainerUpdateArgs : EventArgs {
        public Coor[] HighlightedSquares;
        public HighlightState State;

        public ContainerUpdateArgs(Coor[] highlightedSquares, HighlightState state)
        {
            HighlightedSquares = highlightedSquares;
            State = state;
        }
    }
}
