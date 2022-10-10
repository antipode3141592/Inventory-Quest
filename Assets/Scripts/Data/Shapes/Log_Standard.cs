using System.Collections.Generic;

namespace Data.Shapes
{
    public class Log_Standard : Shape
    {
        public Log_Standard(Facing defaultFacing = Facing.Right)
        {
            Id = "log_standard";
            MinoCount = 40;
            IsChiral = false;
            IsRotationallySymmetric = false;
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask(new bool[,] { 
                    {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
                    {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true}
                }) },
                { Facing.Down, new BitMask(new bool[,] { { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }}) },
                { Facing.Left, new BitMask(new bool[,] { 
                    {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
                    {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true} 
                }) },
                { Facing.Up, new BitMask(new bool[,] { { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }, { true, true }}) },
            };
            CurrentFacing = defaultFacing;
        }
    }
}
