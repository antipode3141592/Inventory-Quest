using UnityEngine;
using Data.Shapes;
using System.Collections.Generic;
using System;

namespace Data.Items

{
    public interface IItem
    {
        public string GuId { get; }  //unique-ish object identifier
        public string Id { get; }    //descriptive name for logging
        public float Weight { get; }   //item weight
        public float Value { get; }    //item value
        public IItemStats Stats { get; }

        public Sprite Sprite { get; set; }

        public IShape Shape { get; }

        public Facing CurrentFacing { get; }

        public int Quantity { get; set; }

        public IDictionary<Type, IItemComponent> Components { get; }

        public void Rotate(int direction);

        public event EventHandler RequestDestruction;
    }
}
