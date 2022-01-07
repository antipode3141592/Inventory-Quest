using InventoryQuest;
using InventoryQuest.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data

{
    public class Item
    {
        public string Id;
        public ItemStats ItemStats;
        public Shape ItemShape;

        public Item(string id, ItemStats itemStats, Shape itemShape)
        {
            Id = id;
            ItemStats = itemStats;
            ItemShape = itemShape;
        }



        //public virtual void Discard();
        //public virtual void Pickup();
    }
}
