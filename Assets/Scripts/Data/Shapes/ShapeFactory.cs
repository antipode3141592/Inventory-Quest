
using Data.Shapes;

namespace Data
{
    public class ShapeFactory
    {
        public static Shape GetShape(ShapeType shape, Facing facing)
        {
            Shape _shape;
            switch (shape)
            {
                case ShapeType.Monomino:
                    _shape = new Monomino(facing);
                    break;
                case ShapeType.Domino:
                    _shape = new Domino(facing);
                    break;
                case ShapeType.Tromino_I:
                    _shape = new Tromino_I(facing);
                    break;
                case ShapeType.Tromino_L:
                    _shape = new Tromino_L(facing);
                    break;
                case ShapeType.Tetromino_O:
                    _shape = new Tetromino_O(facing);
                    break;
                case ShapeType.Tetromino_T:
                    _shape = new Tetromino_T(facing);
                    break;
                case ShapeType.Tetromino_I:
                    _shape = new Tetromino_I(facing);
                    break;
                default:
                    _shape = new Monomino(facing);
                    break;
            }
            return _shape;
        }
    }
}
