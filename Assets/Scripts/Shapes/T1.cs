using Data;
using System.Collections.Generic;

namespace InventoryQuest.Shapes
{
    public class T1 : Shape
    {
        public T1(Facing defaultFacing = Facing.Right)
        {
            Id = "t1";
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
    }

    
}
