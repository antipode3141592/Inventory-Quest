using Data.Interfaces;
using System.Collections.Generic;

namespace Data
{
    public abstract class Shape : IRotatable
    {
        public string Id;
        public Dictionary<Facing, BitMask> Masks;
        public Facing CurrentFacing;

        public BitMask CurrentMask => Masks[CurrentFacing];

        public Coor Size => new Coor(Masks[CurrentFacing].Map.GetLength(0), Masks[CurrentFacing].Map.GetLength(1));

        public virtual void Rotate(int direction)
        {
            //update facing
            CurrentFacing = (Facing)(((int)CurrentFacing + direction) % Masks.Count);
        }
    }
}
