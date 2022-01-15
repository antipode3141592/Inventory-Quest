using Data;
using InventoryQuest.Shapes;

namespace InventoryQuest
{
    public class ContainerFactory
    {
        public static Container GetContainer(ShapeType shape, ItemStats stats, Coor containerSize)
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
                    _shape = new T1();
                    break;
                default:
                    _shape = new Square1();
                    break;
            }
            return new Container(itemStats: stats, shape: _shape, containerSize: containerSize);
        }
    }
}
