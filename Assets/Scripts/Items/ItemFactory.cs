using Data;
using InventoryQuest.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
