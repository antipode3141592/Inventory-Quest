using Data.Items;
using System;
using System.Collections.Generic;

namespace Data.Characters
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

        public ICharacterStats GetById(string id)
        {

            return id switch
            {
                "Player" => DefaultPlayerStats(),
                "Minion" => DefaultMinionStats(),
                "stanley" => DefaultMinionStats(_name: "Stanley", _id: "stanley", _portraitPath: "Portraits/Enemy 05-1"),
                "messenger_dispatcher" => DefaultMinionStats(_name: "Dispatch", _id: "messenger_dispatcher", _portraitPath: "Portraits/Enemy 22"),
                "scummy_overseer" => DefaultMinionStats(_name:"Overseer", _id: "scummy_overseer", _portraitPath: "Portraits/Enemy 04-1"),
                _ => DefaultPlayerStats(),
            };
        }

        public CharacterStats DefaultPlayerStats()
        {
            EquipmentSlotType[] equipmentSlots = GetDefaultEquipmentSlotTypes();
            string speciesId = "human";

            Dictionary<StatTypes, int> physicalStats = GetStatsBlock(GetSpeciesBaseStats(speciesId));

            return new CharacterStats(
                name: "[PLAYER NAME]",
                id: "player",
                portraitPath: "Portraits/Enemy 01-1", 
                speciesId: speciesId,
                stats: physicalStats, 
                equipmentSlots: equipmentSlots);
        }

        public CharacterStats DefaultMinionStats()
        {
            EquipmentSlotType[] equipmentSlots = GetDefaultEquipmentSlotTypes();

            string speciesId = "orc";

            Dictionary<StatTypes, int> physicalStats = GetStatsBlock(GetSpeciesBaseStats(speciesId));

            return new CharacterStats(
                name: "Minion",
                id: "minion",
                portraitPath: "Portraits/Enemy 03-1", 
                speciesId: speciesId,
                stats: physicalStats, 
                equipmentSlots: equipmentSlots);
        }

        public CharacterStats DefaultMinionStats(string _name, string _id, string _portraitPath)
        {
            EquipmentSlotType[] equipmentSlots = GetDefaultEquipmentSlotTypes();

            string speciesId = "orc";

            Dictionary<StatTypes, int> physicalStats = GetStatsBlock(GetSpeciesBaseStats(speciesId));

            return new CharacterStats(
                name: _name,
                id: _id,
                portraitPath: _portraitPath,
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

        Dictionary<StatTypes, int> GetStatsBlock(int[] stats)
        {
            Dictionary<StatTypes, int> physicalStats = new()
            {
                { StatTypes.Strength, stats[0] },
                { StatTypes.Vitality, stats[1] },
                { StatTypes.Agility, stats[2] },
                { StatTypes.Speed, stats[3] },
                { StatTypes.Charisma, stats[4] },
                { StatTypes.Intellect, stats[5] },
                { StatTypes.Spirit, stats[6] },
                { StatTypes.Arcane, stats[7] }
            };
            return physicalStats;
        }

        int[] GetSpeciesBaseStats(string id)
        {
            if (SpeciesBaseStats.ContainsKey(id))
                return SpeciesBaseStats[id];
            return SpeciesBaseStats["human"];   //some serious human bias here
        }

        public ICharacterStats GetRandom()
        {
            throw new NotImplementedException();
        }
    }
}
