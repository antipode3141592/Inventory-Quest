using System.Collections.Generic;

namespace Data.Shapes
{
    public interface IShape
    {
        public string Id { get; }
        public IDictionary<Facing, HashSet<Coor>> Points { get; }
        public bool IsChiral { get; }
        public bool IsRotationallySymmetric { get; }
        public int MinoCount { get; }
    }
}