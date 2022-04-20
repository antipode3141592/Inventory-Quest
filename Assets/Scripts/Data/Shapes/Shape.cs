using System.Collections.Generic;

namespace Data.Shapes
{
    public abstract class Shape : IRotatable
    {
        public string Id;
        public Dictionary<Facing, BitMask> Masks;
        public Facing CurrentFacing;
        public bool IsRotationallySymmetric { get; protected set; }
        public bool IsChiral { get; protected set; }
        public int MinoCount { get; protected set; }
        public BitMask CurrentMask => Masks[CurrentFacing];

        public Coor Size => new Coor(Masks[CurrentFacing].Map.GetLength(0), Masks[CurrentFacing].Map.GetLength(1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction">+1 CW, -1 CCW</param>
        public Facing Rotate(int direction)
        {
            //update facing

            int v = (int)CurrentFacing + direction;
            
            var endFacing = v % Masks.Count < 0 ? (Facing)(Masks.Count - 1) : (Facing)(v % Masks.Count);
            CurrentFacing = endFacing;
            return endFacing;
        }
    }
}
