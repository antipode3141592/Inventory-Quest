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
        

        public DataSourceTest()
        {

        }

        public IItemStats GetRandomItemStats(Rarity rarity) 
        {

            return null;
        }

        public CharacterStats GetCharacterStats(string id)
        {

            return id switch
            {
                "Player" => DefaultPlayerStats(),
                _ => DefaultPlayerStats(),
            };
        }

        public CharacterStats DefaultPlayerStats()
        {
            EquipmentSlotType[] equipmentSlots = { 
                EquipmentSlotType.Belt, 
                EquipmentSlotType.Feet 
            };            

            KeyValuePair<StatType, float>[] physicalStats = new KeyValuePair<StatType, float>[] {
                new(StatType.Strength, 10f),
                new(StatType.Dexterity, 10f),
                new(StatType.Durability, 10f)
            };

            return new CharacterStats(stats: physicalStats, equipmentSlots: equipmentSlots);
        }
    }


}
