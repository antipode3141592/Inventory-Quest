using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Zenject;
using UnityEngine;

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
                EquipmentSlotType.RightHand, 
                EquipmentSlotType.LeftHand,
                EquipmentSlotType.Belt, 
                EquipmentSlotType.Feet 
            };

            Dictionary<Type, float> physicalStats = new()
            {
                { typeof(Strength), 10f },
                { typeof(Dexterity), 10f },
                { typeof(Durability), 10f },
                { typeof(Charisma), 10f }
            };

            return new CharacterStats(stats: physicalStats, equipmentSlots: equipmentSlots);
        }

        public IItemStats GetItemStats(string id)
        {
            return id switch {
                "apple_fuji" => new ItemStats("apple_fuji",
                     weight: .1f,
                     goldValue: .5f,
                     description: "Fuji Apple, the objectively best apple",
                     shape: ShapeType.Square1),

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
                "basic_sword_1" => new EquipableItemStats("basic_sword_1",
                     weight: 2f,
                     goldValue: 10f,
                     description: "a basic sword",
                     shape: ShapeType.Bar3,
                     defaultFacing: Facing.Down,
                     modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,1f),
                        new(typeof(Charisma), OperatorType.Multiply, 2f)}
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
