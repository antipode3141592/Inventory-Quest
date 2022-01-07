using Data;
using System.Collections.Generic;

namespace InventoryQuest.Shapes
{
    public class Square2 : Shape
    {
        public Square2()
        {
            Id = "square2";
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask( new bool[,] { { true, true }, { true, true } }) },
                { Facing.Down, new BitMask( new bool[,] { { true, true }, { true, true } }) },
                { Facing.Left, new BitMask( new bool[,] { { true, true }, { true, true } }) },
                { Facing.Up, new BitMask( new bool[,] { { true, true }, { true, true } }) },
            };   
        }
    }

    
}
