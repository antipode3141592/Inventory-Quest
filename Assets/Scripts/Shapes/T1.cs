using Data;
using System.Collections.Generic;

namespace InventoryQuest.Shapes
{
    public class T1 : Shape
    {
        public T1(Facing defaultFacing)
        {
            Id = "t1";
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask(new bool[,] { { false, true, false }, { false, true, true }, { false, true, false } }) },
                { Facing.Down, new BitMask(new bool[,] { { false, false, false }, { true, true, true }, { false, true, false } }) },
                { Facing.Left, new BitMask(new bool[,] { { false, true, false }, { true, true, false }, { false, true, false } }) },
                { Facing.Up, new BitMask(new bool[,] { { false, true, false }, { true, true, true }, { false, false, false } }) }
            };
        }
    }

    
}
