using System;

namespace Data
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
