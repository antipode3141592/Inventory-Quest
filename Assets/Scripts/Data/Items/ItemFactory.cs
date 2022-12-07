using Data.Items.Components;

namespace Data.Items
{
    public class ItemFactory
    {
        public static IItem GetItem(IItemStats itemStats)
        {
            IItem item = new Item(itemStats: itemStats);
            return AddComponents(itemStats, item);

        }

        public static IItem AddComponents(IItemStats itemStats, IItem item)
        {
            if (itemStats.Components is null) return item;
            foreach (var component in itemStats.Components)
            {
                IEquipableStats equipableStats = component as IEquipableStats;
                if (equipableStats is not null)
                {
                    item.Components.Add(typeof(IEquipable), new Equipable(equipableStats, item));
                    continue;
                }
                IContainerStats containerStats = component as IContainerStats;
                if (containerStats is not null)
                {
                    item.Components.Add(typeof(IContainer), new Container(containerStats, item));
                    continue;
                }
                IStackableStats stackableItemStats = component as IStackableStats;
                if (stackableItemStats is not null)
                {
                    item.Components.Add(typeof(IStackable), new Stackable(stackableItemStats, item));
                    continue;
                }
                IUsableStats usableStats = component as IUsableStats;
                if (usableStats is not null)
                {
                    if (usableStats is EdibleStats edibleStats)
                        item.Components.Add(typeof(IUsable), new Edible(edibleStats, item));
                    if (usableStats is EncounterLengthEffectStats encounterLengthEffectStats)
                        item.Components.Add(typeof(IUsable), new EncounterLengthEffect(encounterLengthEffectStats, item));
                }
            }
            return item;
        }
    }
}
