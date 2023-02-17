using System.Collections.Generic;
using Data;
using Data.Shapes;

namespace InventoryQuest.Testing.Stubs
{
    public class Monomino : IShape
    {
        public Monomino(Facing defaultFacing = Facing.Right)
        {
            Id = "monomino";
            MinoCount = 1;
            IsChiral = false;
            IsRotationallySymmetric = true;
            Points = new Dictionary<Facing, HashSet<Coor>>
            {
                {Facing.Right, new() {new Coor(0,0) } },
                {Facing.Down, new() {new Coor(0,0) } },
                {Facing.Left, new() {new Coor(0,0) } },
                {Facing.Up, new() {new Coor(0,0) } }
            };
        }
        public string Id { get; }
        public IDictionary<Facing, HashSet<Coor>> Points { get; }
        public bool IsChiral { get; }
        public bool IsRotationallySymmetric { get; }
        public int MinoCount { get; }
    
    }
}
