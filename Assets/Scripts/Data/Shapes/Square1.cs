﻿using System.Collections.Generic;

namespace Data.Shapes
{
    public class Square1 : Shape
    {
        public Square1(Facing defaultFacing = Facing.Right)
        {
            Id = "square1";
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
