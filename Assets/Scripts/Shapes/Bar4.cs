using Data;
using System.Collections.Generic;

namespace InventoryQuest.Shapes
{
    public class Bar4 : Shape
    {
        public Bar4(Facing defaultFacing)
        {
            Id = "bar4";
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask(new bool[,] { {true, true, true, true} }) },
                { Facing.Down, new BitMask(new bool[,] { { true }, { true }, { true }, { true }}) },
                { Facing.Left, new BitMask(new bool[,] { {true, true, true, true} }) },
                { Facing.Up, new BitMask(new bool[,] { { true }, { true }, { true }, { true }}) },
            };
        }
    }

    
}
