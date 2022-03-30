
using Data.Shapes;

namespace Data
{
    public class ShapeFactory
    {
        public static Shape GetShape(ShapeType shape, Facing facing = Facing.Right)
        {
            Shape _shape;
            switch (shape)
            {
                case ShapeType.Monomino:
                    _shape = new Monomino();
                    break;
                case ShapeType.Domino:
                    _shape = new Domino();
                    break;
                case ShapeType.Tromino_I:
                    _shape = new Tromino_I();
                    break;
                //case ShapeType.Tromino_L:
                //    _shape = new Tromino_L();
                //    break;
                case ShapeType.Tetromino_O:
                    _shape = new Tetromino_O();
                    break;
                case ShapeType.Tetromino_T:
                    _shape = new Tetromino_T(facing);
                    break;
                case ShapeType.Tetromino_I:
                    _shape = new Tetromino_I(facing);
                    break;
                default:
                    _shape = new Monomino();
                    break;
            }
            return _shape;
        }
    }
}
