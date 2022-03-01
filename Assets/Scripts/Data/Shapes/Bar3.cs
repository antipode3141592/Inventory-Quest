using System.Collections.Generic;

namespace Data.Shapes
{
    public class Bar3 : Shape
    {
        public Bar3(Facing defaultFacing = Facing.Right)
        {
            Id = "bar3";
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask(new bool[,] { {true, true, true} }) },
                { Facing.Down, new BitMask(new bool[,] { { true }, { true }, { true }}) },
                { Facing.Left, new BitMask(new bool[,] { {true, true, true} }) },
                { Facing.Up, new BitMask(new bool[,] { { true }, { true }, { true }}) },
            };
            CurrentFacing = defaultFacing;
        }
    }

    
}
