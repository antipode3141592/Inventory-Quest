using System;
using System.Collections.Generic;

namespace Data.Stats
{
    public class CharacterDataSourceTest : ICharacterDataSource
    {
        Dictionary<string, int[]> SpeciesBaseStats = new Dictionary<string, int[]>()
        {
            {"human", new int[] { 10, 10, 10, 10, 10, 10, 10, 10 } },
            {"orc", new int[] { 12, 12, 12, 12, 10, 10, 6, 6 } },
            {"goblin", new int[] { 8, 8, 12, 12, 8, 12, 10, 10 } },
            {"dryad", new int[] { 8, 12, 8, 8, 12, 12, 12, 8 } },
            {"mer", new int[] { 12, 8, 12, 8, 10, 10, 8, 12 } },
            {"tooloo", new int[] { 6, 14, 6, 6, 12, 12, 12, 12 } },
            {"octopod", new int[] { 12, 8, 12, 8, 8, 14, 6, 12 } },
            {"troll", new int[] { 14, 12, 8, 8, 10, 8, 12, 8 } }
        };

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
            string speciesId = "human";

            Dictionary<Type, int> physicalStats = GetStatsBlock(GetSpeciesBaseStats(speciesId));

            return new CharacterStats(
                name: "[PLAYER NAME]", 
                portraitPath: "Portraits/Enemy 01-1", 
                speciesId: speciesId,
                stats: physicalStats, 
                equipmentSlots: equipmentSlots);
        }

        public CharacterStats DefaultMinionStats()
        {
            EquipmentSlotType[] equipmentSlots = GetDefaultEquipmentSlotTypes();

            string speciesId = "orc";

            Dictionary<Type, int> physicalStats = GetStatsBlock(GetSpeciesBaseStats(speciesId));

            return new CharacterStats(
                name: "Minion", 
                portraitPath: "Portraits/Enemy 03-1", 
                speciesId: speciesId,
                stats: physicalStats, 
                equipmentSlots: equipmentSlots);
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

        int[] GetSpeciesBaseStats(string id)
        {
            if (SpeciesBaseStats.ContainsKey(id))
                return SpeciesBaseStats[id];
            return SpeciesBaseStats["human"];   //some serious human bias here
        }
    }


    public static class PlayableCharacterLeveler
    {
        static void AddRanksToCharacter(PlayableCharacter character, IDictionary<Type, int> purchasedRanks)
        {
            var statsDictionary = character.Stats.Stats;
            foreach (var skill in purchasedRanks)
            {
                statsDictionary[skill.Key].PurchasedLevels += skill.Value;
            }
        }
    }
}
