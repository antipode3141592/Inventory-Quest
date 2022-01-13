using System;
using UnityEngine;

namespace Data
{
    public class GridEventArgs: EventArgs
    {
        public Vector2Int gridPosition;

        public GridEventArgs(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
        }
    }

    
}
