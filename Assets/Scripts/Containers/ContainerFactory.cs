using Data;
using InventoryQuest.Shapes;

namespace InventoryQuest
{
    public class ContainerFactory
    {
        public static Container GetContainer(ContainerStats stats)
        {   
            return new Container(stats: stats);
        }
    }
}
