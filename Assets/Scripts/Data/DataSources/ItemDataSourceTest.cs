using Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    public class ItemDataSourceTest : IItemDataSource
    {
        Dictionary<string, IItemStats> itemDictionary = new Dictionary<string, IItemStats>()
        {
            {   "apple_fuji", new ItemStats("apple_fuji",
                     weight: .1f,
                     goldValue: .5f,
                     description: "The objectively best apple.",
                     rarity: Rarity.common,
                     spritePath: "Items/Apple_Fuji",
                     shape: ShapeType.Square1) },
            {   "brass_trinket", new ItemStats("brass_trinket",
                     weight: .3f,
                     goldValue: 1f,
                     description: "A small brass object with no purpose.",
                     rarity: Rarity.common,
                     spritePath: "",
                     shape: ShapeType.Square1) },
            {   "loot_pile_small", new ContainerStats("loot_pile_small",
                    weight: 0f,
                    goldValue: 0f,
                    description: "small pile",
                    spritePath: "",
                    containerSize: new Coor(r: 5, c: 3))
            },
            {    "loot_pile_medium", new ContainerStats("loot_pile_medium",
                    weight: 0f,
                    goldValue: 0f,
                    description: "average pile",
                    spritePath: "",
                    containerSize: new Coor(r: 5, c: 4))
            },
            {    "loot_pile_large", new ContainerStats("loot_pile_large",
                    weight: 0f,
                    goldValue: 0f,
                    description: "large pile",
                    spritePath: "",
                    containerSize: new Coor(r: 6, c: 5))
            },

            {   "adventure backpack", new ContainerStats("adventure backpack",
                    weight: 2f,
                    goldValue: 5f,
                    description: "a basic adventurer's backpack",
                    rarity: Rarity.common,
                    spritePath: "",
                    shape: ShapeType.Square1,
                    containerSize: new Coor(12, 5))
            },
            {

                "small backpack", new ContainerStats("small backpack",
                    weight: 1f,
                    goldValue: 2f,
                    description: "a small backpack",
                    rarity: Rarity.common,
                    spritePath: "",
                    shape: ShapeType.Square1,
                    containerSize: new Coor(8, 4))
                },
            { "basic_sword_1", new EquipableItemStats("basic_sword_1",
                     weight: 2f,
                     goldValue: 10f,
                     description: "a basic sword",
                     rarity: Rarity.common,
                     spritePath: "Items/basic_sword_1",
                     shape: ShapeType.Bar3,
                     slotType: EquipmentSlotType.OneHandedWeapon,
                     defaultFacing: Facing.Down,
                     modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,1)}
                     )
            },
            {   "basic_sword_15", new EquipableItemStats("basic_sword_15",
                    weight: 3.5f,
                    goldValue: 10000f,
                    description: "a basic sword",
                    rarity: Rarity.epic,
                    spritePath: "Items/basic_sword_1",
                    shape: ShapeType.Bar3,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                            new(typeof(Strength),OperatorType.Add,15)}
                    )
            },
            {    "basic_crossbow_1", new EquipableItemStats("basic_crossbow_1",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic crossbow",
                    rarity: Rarity.common,
                    spritePath: "Items/basic_crossbow_1",
                    shape: ShapeType.T1,
                    slotType: EquipmentSlotType.TwoHandedWeapon,
                    defaultFacing: Facing.Right,
                    modifiers: new StatModifier[] {
                            new(typeof(Agility),OperatorType.Add,1)}
                    )
            },
            {   "basic_sword_5", new EquipableItemStats("basic_sword_5",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic sword",
                    rarity: Rarity.rare,
                    spritePath: "",
                    shape: ShapeType.Bar4,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,5)}
                    )
            },
            {   "ring_charisma_5", new EquipableItemStats("ring_charisma_1",
                    weight: 0.1f,
                    goldValue: 500f,
                    description: "A simple silver band that is quite charming.",
                    rarity: Rarity.rare,
                    spritePath: "",
                    shape: ShapeType.Square1,
                    slotType: EquipmentSlotType.Ring,
                    modifiers: new StatModifier[] {
                        new(typeof(Charisma), OperatorType.Add, 5)}
                    )
            },
        };

        public ItemDataSourceTest()
        {

        }

        public IItemStats GetRandomItemStats(Rarity rarity) 
        {
            var itemsOfRarity = itemDictionary.Where(x => x.Value.Rarity == rarity);
            int randomIndex = Random.Range(0,itemsOfRarity.Count());
            return itemsOfRarity.ElementAt(randomIndex).Value;
        }

        

        public IItemStats GetItemStats(string id)
        {
            if (itemDictionary.ContainsKey(id)) return itemDictionary[id];
            return null;
        }

        
    }
}
