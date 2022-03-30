using System.Collections.Generic;

namespace Data.Shapes
{
    public class Tetromino_T : Shape
    {
        public Tetromino_T(Facing defaultFacing = Facing.Right)
        {
            Id = "tetromino_t";
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask(new bool[,] {
                    { true, false }, 
                    { true, true }, 
                    { true, false } }) 
                },
                { Facing.Down, new BitMask(new bool[,] { 
                    { true, true, true }, 
                    { false, true, false } }) 
                },
                { Facing.Left, new BitMask(new bool[,] { 
                    { false, true }, 
                    { true, true}, 
                    { false, true } 
                }) },
                { Facing.Up, new BitMask(new bool[,] { 
                    { false, true, false }, 
                    { true, true, true } 
                }) }
            };
            CurrentFacing = defaultFacing;
        }
        public override bool IsRotationallySymmetric => false;
    }

    
}
