using Data.Interfaces;
using Data.Stats;
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
                     shape: ShapeType.Monomino) },
            {   "brass_trinket", new ItemStats("brass_trinket",
                     weight: .3f,
                     goldValue: 1f,
                     description: "A small brass object with no purpose.",
                     rarity: Rarity.common,
                     spritePath: "Items/Loot_75",
                     shape: ShapeType.Monomino) },
            {   "book_common", new ItemStats("book_common",
                     weight: .3f,
                     goldValue: 1f,
                     description: "A collection of short stories.",
                     rarity: Rarity.common,
                     spritePath: "Items/book_1",
                     shape: ShapeType.Tetromino_O) },
            {   "ingot_common", new ItemStats("ingot_common",
                     weight: .1f,
                     goldValue: .5f,
                     description: "a decent chunk of iron, ready to forge",
                     rarity: Rarity.common,
                     spritePath: "Items/common_ingot",
                     shape: ShapeType.Domino) },
            {   "ingot_uncommon", new ItemStats("ingot_uncommon",
                     weight: .1f,
                     goldValue: .5f,
                     description: "a nice chunk of Flavorite, ready to forge",
                     rarity: Rarity.uncommon,
                     spritePath: "Items/uncommon_ingot",
                     shape: ShapeType.Domino) },
            {   "loot_pile_small", new ContainerStats("loot_pile_small",
                    weight: 0f,
                    goldValue: 0f,
                    description: "small pile",
                    spritePath: "Items/loot_pile_small",
                    isQuestItem: true,
                    containerSize: new Coor(r: 5, c: 3))
            },
            {    "loot_pile_medium", new ContainerStats("loot_pile_medium",
                    weight: 0f,
                    goldValue: 0f,
                    description: "average pile",
                    spritePath: "Items/loot_pile_medium",
                    isQuestItem: true,
                    containerSize: new Coor(r: 5, c: 4))
            },
            {    "loot_pile_large", new ContainerStats("loot_pile_large",
                    weight: 0f,
                    goldValue: 0f,
                    description: "large pile",
                    spritePath: "Items/loot_pile_large",
                    isQuestItem: true,
                    containerSize: new Coor(r: 6, c: 5))
            },
            {    "loot_pile_gigantic", new ContainerStats("loot_pile_gigantic",
                    weight: 0f,
                    goldValue: 0f,
                    description: "a truly massive pile",
                    spritePath: "Items/loot_pile_gigantic",
                    isQuestItem: true,
                    containerSize: new Coor(r: 12, c: 6))
            },

            {   "adventure backpack", new EquipableContainerStats("adventure backpack",
                    weight: 2f,
                    goldValue: 5f,
                    description: "a basic adventurer's backpack",
                    spritePath: ShapeType.Monomino.ToString(),
                    isQuestItem: true,
                    shapeType: ShapeType.Monomino,
                    containerSize: new Coor(20, 12),
                    slotType: EquipmentSlotType.Backpack
                )
            },
            {

                "small backpack", new EquipableContainerStats("small backpack",
                    weight: 1f,
                    goldValue: 2f,
                    description: "a small backpack",
                    spritePath: ShapeType.Monomino.ToString(),
                    isQuestItem: true,
                    shapeType: ShapeType.Monomino,
                    containerSize: new Coor(8, 4),
                    slotType: EquipmentSlotType.Backpack
                )
            },
            { "basic_sword_1", new EquipableItemStats("basic_sword_1",
                     weight: 2f,
                     goldValue: 10f,
                     description: "a basic sword",
                     rarity: Rarity.common,
                     spritePath: "Items/basic_sword_1",
                     shapeType: ShapeType.Tromino_I,
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
                    shapeType: ShapeType.Tromino_I,
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
                    shapeType: ShapeType.Tetromino_T,
                    slotType: EquipmentSlotType.TwoHandedWeapon,
                    defaultFacing: Facing.Right,
                    modifiers: new StatModifier[] {
                            new(typeof(Agility),OperatorType.Add,1)}
                    )
            },
            {   "basic_shortsword_1", new EquipableItemStats("basic_sword_1",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic shortsword with a sharp edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/common_shortsword_1",
                    shapeType: ShapeType.Tetromino_I,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,1)}
                    )
            },
            {   "basic_shortsword_3", new EquipableItemStats("basic_sword_3",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic shortsword with a sharp edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/uncommon_shortsword_1",
                    shapeType: ShapeType.Tetromino_I,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,3)}
                    )
            },
            {   "basic_sword_3", new EquipableItemStats("basic_sword_3",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic sword with a sharp edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/uncommon_sword_1",
                    shapeType: ShapeType.Tetromino_I,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,5)}
                    )
            },
            {   "basic_sword_5", new EquipableItemStats("basic_sword_5",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic sword with a sharp edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/basic_sword_1",
                    shapeType: ShapeType.Tetromino_I,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,5)}
                    )
            },
            {   "ring_charisma_5", new EquipableItemStats("ring_charisma_5",
                    weight: 0.1f,
                    goldValue: 500f,
                    description: "A simple silver band that is quite charming.",
                    rarity: Rarity.rare,
                    spritePath: "Items/Loot_64",
                    shapeType: ShapeType.Monomino,
                    slotType: EquipmentSlotType.Ring,
                    modifiers: new StatModifier[] {
                        new(typeof(Charisma), OperatorType.Add, 5)}
                    )
            },
            {   "ring_spirit_25", new EquipableItemStats("ring_spirit_25",
                    weight: 0.1f,
                    goldValue: 500f,
                    description: "A golden ring that radiates spirit energy",
                    rarity: Rarity.rare,
                    spritePath: "Items/Loot_64",
                    shapeType: ShapeType.Monomino,
                    slotType: EquipmentSlotType.Ring,
                    modifiers: new StatModifier[] {
                        new(typeof(Spirit), OperatorType.Add, 15)}
                    )
            },
            {   "boot_1", new EquipableItemStats("boot_1",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic shortsword with a sharp edge",
                    rarity: Rarity.common,
                    spritePath: "Items/boot_1",
                    shapeType: ShapeType.Tromino_L,
                    slotType: EquipmentSlotType.Feet)
            },
            {   "shoe_1", new EquipableItemStats("basic_sword_1",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic shortsword with a sharp edge",
                    rarity: Rarity.common,
                    spritePath: "Items/shoe_1",
                    shapeType: ShapeType.Domino,
                    slotType: EquipmentSlotType.Feet)
            },
            {   "sandal_1", new EquipableItemStats("sandal_1",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic shortsword with a sharp edge",
                    rarity: Rarity.common,
                    spritePath: "Items/sandal_1",
                    shapeType: ShapeType.Domino,
                    slotType: EquipmentSlotType.Feet)
            },
        };

        public ItemDataSourceTest()
        {

        }

        public IItemStats GetRandomItemStats(Rarity rarity) 
        {
            var itemsOfRarity = itemDictionary.Select(y => y).Where(x => x.Value.Rarity == rarity && x.Value.IsQuestItem == false);
            int randomIndex = Random.Range(0,itemsOfRarity.Count());
            Debug.Log($"rarity: {rarity}, index: {randomIndex}, itemcount: {itemsOfRarity.Count()}");
            return itemsOfRarity.ElementAt(randomIndex).Value;
        }

        

        public IItemStats GetItemStats(string id)
        {
            if (itemDictionary.ContainsKey(id)) return itemDictionary[id];
            return null;
        }

        
    }
}
