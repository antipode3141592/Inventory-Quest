
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
                case ShapeType.Square1:
                    _shape = new Square1();
                    break;
                case ShapeType.Square2:
                    _shape = new Square2();
                    break;
                case ShapeType.T1:
                    _shape = new T1(facing);
                    break;
                case ShapeType.Bar2:
                    _shape = new Bar2(facing);
                    break;
                case ShapeType.Bar3:
                    _shape = new Bar3(facing);
                    break;
                case ShapeType.Bar4:
                    _shape = new Bar4(facing);
                    break;
                default:
                    _shape = new Square1();
                    break;
            }
            return _shape;
        }
    }
}
