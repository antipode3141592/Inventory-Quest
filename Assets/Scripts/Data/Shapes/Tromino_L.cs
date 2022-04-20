using System.Collections.Generic;

namespace Data.Shapes
{
    public class Tromino_L : Shape
    {
        public Tromino_L(Facing defaultFacing = Facing.Right)
        {
            Id = "tetromino_l";
            MinoCount = 3;
            IsChiral = false;
            IsRotationallySymmetric = false;
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask( new bool[,] { { true, false }, { true, true } }) },
                { Facing.Down, new BitMask( new bool[,] { { true, true }, { true, false } }) },
                { Facing.Left, new BitMask( new bool[,] { { true, true }, { false, true } }) },
                { Facing.Up, new BitMask( new bool[,] { { false, true }, { true, true } }) },
            };
            CurrentFacing = defaultFacing;
        }
    }
}
