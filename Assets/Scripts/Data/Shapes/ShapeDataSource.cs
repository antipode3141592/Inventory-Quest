using Data.Shapes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;

namespace Data.Shapes
{
    public class ShapeDataSource : SerializedMonoBehaviour, IShapeDataSource
    {
        [OdinSerialize] Dictionary<string, IShape> shapes;

        public IShape GetById(string id)
        {
            if (shapes.ContainsKey(id))
                return shapes[id];
            return null;
        }

        public IShape GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
