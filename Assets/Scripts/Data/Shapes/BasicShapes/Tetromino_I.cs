using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

namespace Data.Shapes
{
    class Tetromino_I : SerializedMonoBehaviour
    {
        [OdinSerialize]
        Dictionary<Facing, bool[,]> Masks = new Dictionary<Facing, bool[,]>
            {
                { Facing.Right, new bool[,] { {true, true, true, true} } },
                { Facing.Down, new bool[,] { { true }, { true }, { true }, { true } } },
                { Facing.Left, new bool[,] { { true, true, true, true } } },
                { Facing.Up, new bool[,] { { true }, { true }, { true }, { true } } },
            };
    }

    public class BoolMatrix
    {
        [OdinSerialize]
        [TableMatrix(SquareCells = true)]
        bool[,] Matrix;
    }
}
