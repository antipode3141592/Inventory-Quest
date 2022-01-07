using Data;
using System.Collections.Generic;

namespace InventoryQuest.Shapes
{
    public class Square1 : Shape
    {
        public Square1()
        {
            Id = "square1";
            Masks = new Dictionary<Facing, BitMask>
            {
                {Facing.Right, new BitMask(new bool[,] { {true } }) },
                {Facing.Down, new BitMask(new bool[,] { {true } }) },
                {Facing.Left, new BitMask(new bool[,] { {true } }) },
                {Facing.Up, new BitMask(new bool[,] { {true } }) }
            };
        }
    }

    
}
