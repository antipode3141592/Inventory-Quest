using System.Collections.Generic;

namespace Data.Shapes
{
    public class Domino : Shape
    {
        public Domino(Facing defaultFacing = Facing.Right)
        {
            Id = "domino";
            MinoCount = 2;
            IsChiral = false;
            IsRotationallySymmetric = false;
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
