using System.Collections.Generic;

namespace Data.Shapes
{
    public class Bar2 : Shape
    {
        public Bar2(Facing defaultFacing = Facing.Right)
        {
            Id = "bar2";
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask(new bool[,] { { true, true } }) },
                { Facing.Down, new BitMask(new bool[,] { { true }, { true } }) },
                { Facing.Left, new BitMask(new bool[,] { { true, true } }) },
                { Facing.Up, new BitMask(new bool[,] { { true }, { true } }) },
            };
            CurrentFacing = defaultFacing;
        }
    }

    
}
