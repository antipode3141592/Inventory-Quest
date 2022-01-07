using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace InventoryQuest.Shapes
{
    public abstract class Shape: IRotatable
    {
        public string Id;
        public Sprite Sprite;

        public Dictionary<Facing, BitMask> Masks;
        public Facing CurrentFacing;


        public BitMask CurrentMask => Masks[CurrentFacing];

        public Vector2Int Size => new Vector2Int(x: Masks[CurrentFacing].Map.GetLength(0), y: Masks[CurrentFacing].Map.GetLength(1));

        public virtual void Rotate(int direction)
        {
            //update facing
            CurrentFacing = (Facing)(((int)CurrentFacing + direction) % Masks.Count);

            //rotate sprite

            //invoke OnRotate
        }
    }
}
