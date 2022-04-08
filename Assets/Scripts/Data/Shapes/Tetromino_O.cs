﻿using System.Collections.Generic;

namespace Data.Shapes
{
    public class Tetromino_O : Shape
    {
        public Tetromino_O(Facing defaultFacing = Facing.Right)
        {
            Id = "tetromino_o";
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask( new bool[,] { { true, true }, { true, true } }) },
                { Facing.Down, new BitMask( new bool[,] { { true, true }, { true, true } }) },
                { Facing.Left, new BitMask( new bool[,] { { true, true }, { true, true } }) },
                { Facing.Up, new BitMask( new bool[,] { { true, true }, { true, true } }) },
            };
            CurrentFacing = defaultFacing;
        }
        public override bool IsRotationallySymmetric => true;
    }

    
}