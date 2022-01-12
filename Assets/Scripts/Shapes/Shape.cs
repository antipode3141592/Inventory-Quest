using Data;
using System.Collections.Generic;

namespace InventoryQuest.Shapes
{
    public abstract class Shape : IRotatable
    {
        public string Id;
        public Dictionary<Facing, BitMask> Masks;
        public Facing CurrentFacing;

        public BitMask CurrentMask => Masks[CurrentFacing];

        public Coor Size {
            get {
                Coor retval;
                retval.row = Masks[CurrentFacing].Map.GetLength(0);
                retval.column = Masks[CurrentFacing].Map.GetLength(1);
                return retval;
            }
        } 

        public virtual void Rotate(int direction)
        {
            //update facing
            CurrentFacing = (Facing)(((int)CurrentFacing + direction) % Masks.Count);
        }
    }
}
