using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Zenject;

namespace InventoryQuest
{
    public class DataSourceTest : IDataSource
    {
        public Dictionary<string, Item> Items;

        public DataSourceTest()
        {
            Items = new Dictionary<string, Item>();

        }

        public Item GetRandomItem(Rarity rarity) 
        {
            throw new NotImplementedException();
        }
    }


}
