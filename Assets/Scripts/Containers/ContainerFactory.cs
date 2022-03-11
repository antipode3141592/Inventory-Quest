using Data;

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
