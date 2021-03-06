using System.Collections.Generic;

namespace Data.Shapes
{
    public class Monomino : Shape
    {
        public Monomino(Facing defaultFacing = Facing.Right)
        {
            Id = "monomino";
            MinoCount = 1;
            IsChiral = false;
            IsRotationallySymmetric = true;
            Masks = new Dictionary<Facing, BitMask>
            {
                {Facing.Right, new BitMask(new bool[,] { {true } }) },
                {Facing.Down, new BitMask(new bool[,] { {true } }) },
                {Facing.Left, new BitMask(new bool[,] { {true } }) },
                {Facing.Up, new BitMask(new bool[,] { {true } }) }
            };
            CurrentFacing = defaultFacing;
        }
    }

    
}
