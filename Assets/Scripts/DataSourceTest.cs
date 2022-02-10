using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Zenject;
using UnityEngine;
using Data.Interfaces;

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
                "Minion" => DefaultMinionStats(),
                _ => DefaultPlayerStats(),
            };
        }

        public CharacterStats DefaultPlayerStats()
        {
            EquipmentSlotType[] equipmentSlots = GetDefaultEquipmentSlotTypes();

            Dictionary<Type, float> physicalStats = GetStatsBlock(new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f });

            return new CharacterStats(name: "[PLAYER NAME]", portraitPath: "Portraits/Enemy 01-1",  stats: physicalStats, equipmentSlots: equipmentSlots);
        }

        public CharacterStats DefaultMinionStats()
        {
            EquipmentSlotType[] equipmentSlots = GetDefaultEquipmentSlotTypes();

            Dictionary<Type, float> physicalStats = GetStatsBlock(new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f });

            return new CharacterStats(name: "Minion", portraitPath: "Portraits/Enemy 03-1", stats: physicalStats, equipmentSlots: equipmentSlots);
        }

        EquipmentSlotType[] GetDefaultEquipmentSlotTypes()
        {
            EquipmentSlotType[] slots = {
                EquipmentSlotType.RightHand, 
                EquipmentSlotType.LeftHand,
                EquipmentSlotType.Belt, 
                EquipmentSlotType.Feet
            };
            return slots;
        }

        Dictionary<Type, float> GetStatsBlock(float[] stats)
        {
            Dictionary<Type, float> physicalStats = new()
            {
                { typeof(Strength), stats[0] },
                { typeof(Dexterity), stats[1] },
                { typeof(Durability), stats[2] },
                { typeof(Charisma), stats[3] },
                { typeof(Speed), stats[4] },
                { typeof(Intelligence), stats[5] },
                { typeof(Wisdom), stats[6] }
            };
            return physicalStats;
        } 

        public IItemStats GetItemStats(string id)
        {
            return id switch {
                "apple_fuji" => new ItemStats("apple_fuji",
                     weight: .1f,
                     goldValue: .5f,
                     description: "Fuji Apple, the objectively best apple",
                     shape: ShapeType.Square1),
                "loot_pile" => new ContainerStats("loot_pile",
                    weight: 0f,
                    goldValue: 0f,
                    description: "current loot pile",
                    containerSize: new Coor(r: 3, c: 5)),
                "thingabob" => new ItemStats("thingabob",
                     weight: 4f,
                     goldValue: 5f,
                     description: "a strange shape",
                     shape: ShapeType.T1),
                "adventure backpack" => new ContainerStats("adventure backpack",
                    weight: 2f,
                    goldValue: 5f,
                    description: "a basic adventurer's backpack",
                    shape: ShapeType.Square1,
                    containerSize: new Coor(5, 12)),
                "small backpack" => new ContainerStats("small backpack",
                    weight: 1f,
                    goldValue: 2f,
                    description: "a small backpack",
                    shape: ShapeType.Square1,
                    containerSize: new Coor(4, 8)),
                "basic_sword_1" => new EquipableItemStats("basic_sword_1",
                     weight: 2f,
                     goldValue: 10f,
                     description: "a basic sword",
                     shape: ShapeType.Bar3,
                     defaultFacing: Facing.Down,
                     modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,1f)}
                     ),
                "basic_sword_5" => new EquipableItemStats("basic_sword_5",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic sword",
                    shape: ShapeType.Bar4,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,5f)}
                    ),
                "ring_charisma_1" => new EquipableItemStats("ring_charisma_1",
                    weight: 0.1f,
                    goldValue: 50f,
                    description: "simple silver band which grants minor charisma boost",
                    shape: ShapeType.Square1, 
                    modifiers: new StatModifier[] {
                        new(typeof(Charisma), OperatorType.Add, 1f)}
                    ),
                _ => new ItemStats("blop",
                     weight: .1f,
                     goldValue: 0f,
                     description: "blip bloop blop",
                     shape: ShapeType.Square1)
            };
        }

        public CharacterStats GetRandomCharacterStats(Rarity rarity)
        {
            throw new NotImplementedException();
        }
    }


}
