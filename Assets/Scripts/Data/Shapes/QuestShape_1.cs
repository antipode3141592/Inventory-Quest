using System.Collections.Generic;

namespace Data.Shapes
{
    public class QuestShape_1 : Shape
    {
        public QuestShape_1(Facing defaultFacing = Facing.Right)
        {
            Id = "questshape_1";
            MinoCount = 8;
            IsChiral = false;
            IsRotationallySymmetric = false;
            Masks = new Dictionary<Facing, BitMask>
            {
                { Facing.Right, new BitMask(new bool[,] {
                    { true, false, true, true }, 
                    { true, true, true, false }, 
                    { true, false, true, false } 
                }) 
                },
                { Facing.Down, new BitMask(new bool[,] { 
                    { true, true, true }, 
                    { false, true, false },
                    { true, true, true },
                    { false, false, true }
                }) 
                },
                { Facing.Left, new BitMask(new bool[,] {
                    { false, true, false, true },
                    { false, true, true, true },
                    { true, true, false, true }
                }) },
                { Facing.Up, new BitMask(new bool[,] { 
                    { true, false, false },
                    { true, true, true },
                    { false, true, false },
                    { true, true, true }
                }) }
            };
            CurrentFacing = defaultFacing;
        }
    }

    
}
