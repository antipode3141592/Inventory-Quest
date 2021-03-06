using System.Collections.Generic;

namespace Data.Shapes
{
    public class Tromino_I : Shape
    {
        public Tromino_I(Facing defaultFacing = Facing.Right)
        {
            Id = "tromino_i";
            MinoCount = 3;
            IsChiral = false;
            IsRotationallySymmetric = false;
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
