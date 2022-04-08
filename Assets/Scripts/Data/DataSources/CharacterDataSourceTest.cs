using System;
using System.Collections.Generic;

namespace Data.Stats
{
    public class CharacterDataSourceTest : ICharacterDataSource
    {
        public CharacterStats GetCharacterStats(string id)
        {

            return id switch
            {
                "Player" => DefaultPlayerStats(),
                "Minion" => DefaultMinionStats(),
                _ => DefaultPlayerStats(),
            };
        }

        public CharacterStats DefaultPlayerStats()
        {
            EquipmentSlotType[] equipmentSlots = GetDefaultEquipmentSlotTypes();

            Dictionary<Type, int> physicalStats = GetStatsBlock(new int[] { 10, 10, 10, 10, 10, 10, 10, 10 });

            return new CharacterStats(name: "[PLAYER NAME]", portraitPath: "Portraits/Enemy 01-1", stats: physicalStats, equipmentSlots: equipmentSlots);
        }

        public CharacterStats DefaultMinionStats()
        {
            EquipmentSlotType[] equipmentSlots = GetDefaultEquipmentSlotTypes();

            Dictionary<Type, int> physicalStats = GetStatsBlock(new int[] { 5, 5, 5, 5, 5, 5, 0, 0 });

            return new CharacterStats(name: "Minion", portraitPath: "Portraits/Enemy 03-1", stats: physicalStats, equipmentSlots: equipmentSlots);
        }

        EquipmentSlotType[] GetDefaultEquipmentSlotTypes()
        {
            EquipmentSlotType[] slots = {
                EquipmentSlotType.OneHandedWeapon,
                EquipmentSlotType.Shield,
                EquipmentSlotType.Head,
                EquipmentSlotType.InnerTorso,
                EquipmentSlotType.OuterTorso,
                EquipmentSlotType.Legs,
                EquipmentSlotType.Belt,
                EquipmentSlotType.Feet,
                EquipmentSlotType.Neck,
                EquipmentSlotType.Ring,
                EquipmentSlotType.Hands,
                EquipmentSlotType.Backpack
            };
            return slots;
        }

        Dictionary<Type, int> GetStatsBlock(int[] stats)
        {
            Dictionary<Type, int> physicalStats = new()
            {
                { typeof(Strength), stats[0] },
                { typeof(Vitality), stats[1] },
                { typeof(Agility), stats[2] },
                { typeof(Speed), stats[3] },
                { typeof(Charisma), stats[4] },
                { typeof(Intellect), stats[5] },
                { typeof(Spirit), stats[6] },
                { typeof(Arcane), stats[7] }
            };
            return physicalStats;
        }
        public CharacterStats GetRandomCharacterStats(Rarity rarity)
        {
            throw new NotImplementedException();
        }
    }
}
