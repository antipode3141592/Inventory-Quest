using Data;
using Data.Characters;
using Data.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.Testing.Stubs
{
    public class CharacterDataSourceTest : ICharacterDataSource
    {
        Dictionary<string, ISpeciesBaseStats> SpeciesBaseStats = new ()
        {
            {"human", new SpeciesBaseStats(
                id: "human", 
                baseStats: new Dictionary<StatTypes, int>()
                { 
                    {StatTypes.Strength, 10 },
                    {StatTypes.Vitality, 10 },
                    {StatTypes.Agility, 10 },
                    {StatTypes.Speed, 10 },
                    {StatTypes.Charisma, 10 },
                    {StatTypes.Intellect, 10 },
                    {StatTypes.Spirit, 10 },
                    {StatTypes.Arcane, 10 }
                },
                slotTypes: GetDefaultEquipmentSlotTypes()) 
            },
            {"orc",
                new SpeciesBaseStats(
                id: "orc",
                baseStats: new Dictionary<StatTypes, int>()
                {
                    {StatTypes.Strength, 12 },
                    {StatTypes.Vitality, 12 },
                    {StatTypes.Agility, 12 },
                    {StatTypes.Speed, 12 },
                    {StatTypes.Charisma, 10 },
                    {StatTypes.Intellect, 10 },
                    {StatTypes.Spirit, 6 },
                    {StatTypes.Arcane, 6 }
                },
                slotTypes: GetDefaultEquipmentSlotTypes())
            }
        };

        public ICharacterStats GetById(string id)
        {

            return id.ToLower() switch
            {
                "player" => DefaultPlayerStats(),
                "minion" => DefaultMinionStats(),
                "stanley" => DefaultMinionStats(_name: "Stanley", _id: "stanley", _portrait: Resources.Load<Sprite>("Portraits/Enemy 05-1")),
                "messenger_dispatcher" => DefaultMinionStats(_name: "Dispatch", _id: "messenger_dispatcher", _portrait: Resources.Load<Sprite>("Portraits/Enemy 22")),
                "scummy_overseer" => DefaultMinionStats(_name:"Overseer", _id: "scummy_overseer", _portrait: Resources.Load<Sprite>("Portraits/Enemy 04-1")),
                _ => DefaultPlayerStats(),
            };
        }

        public CharacterStats DefaultPlayerStats()
        {
            var equipmentSlots = GetDefaultEquipmentSlotTypes();
            string speciesId = "human";

            return new CharacterStats(
                name: "[PLAYER NAME]",
                id: "player",
                portrait: Resources.Load<Sprite>("Portraits/Enemy 01-1"),
                species: GetSpeciesBaseStats(speciesId),
                initialStats: new(),
                equipmentSlots: equipmentSlots);
        }

        public CharacterStats DefaultMinionStats()
        {
            var equipmentSlots = GetDefaultEquipmentSlotTypes();

            string speciesId = "orc";

            return new CharacterStats(
                name: "Minion",
                id: "minion",
                portrait: Resources.Load<Sprite>("Portraits/Enemy 03-1"), 
                species: GetSpeciesBaseStats(speciesId),
                initialStats: new(), 
                equipmentSlots: equipmentSlots);
        }

        public CharacterStats DefaultMinionStats(string _name, string _id, Sprite _portrait)
        {
            var equipmentSlots = GetDefaultEquipmentSlotTypes();

            string speciesId = "orc";

            return new CharacterStats(
                name: _name,
                id: _id,
                portrait: _portrait,
                species: GetSpeciesBaseStats(speciesId),
                initialStats: new(),
                equipmentSlots: equipmentSlots);
        }

        static List<EquipmentSlotType> GetDefaultEquipmentSlotTypes()
        {
            List<EquipmentSlotType> slots = new()
            {
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
                EquipmentSlotType.Ring,
                EquipmentSlotType.Hands,
                EquipmentSlotType.Backpack
            };
            return slots;
        }

        ISpeciesBaseStats GetSpeciesBaseStats(string id)
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
