using Data;

namespace InventoryQuest
{
    public class ItemFactory
    {
        public static IItem GetItem(IItemStats stats)
        {
            EquipableItemStats equipableStats = stats as EquipableItemStats;
            if (equipableStats != null)
            {
                return new EquipableItem(equipableStats);
            }
            ContainerStats containerStats = stats as ContainerStats;
            if(containerStats != null)
            {
                return new Container(containerStats);
            }

            ItemStats itemStats = stats as ItemStats;
            if (itemStats != null)
            {
                return new Item(itemStats: itemStats);
            }
            return null;
            
        }
    }
}
