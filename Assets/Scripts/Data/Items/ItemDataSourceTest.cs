using Data.Characters;
using Data.Shapes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.Items
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
            {   "book_common", new StackableItemStats("book_common",
                     weight: .3f,
                     goldValue: 1f,
                     description: "A collection of short stories.",
                     rarity: Rarity.common,
                     spritePath: "Items/book_1",
                     shapeType: ShapeType.Tetromino_O) },
            {   "ingot_common", new StackableItemStats("ingot_common",
                     weight: 2.5f,
                     goldValue: .5f,
                     description: "a decent chunk of iron, ready to forge",
                     rarity: Rarity.common,
                     spritePath: "Items/common_ingot",
                     shapeType: ShapeType.Domino,
                     defaultFacing: Facing.Down)},
            {   "ingot_uncommon", new StackableItemStats("ingot_uncommon",
                     weight: 3f,
                     goldValue: 1f,
                     description: "a nice chunk of Flavorite, ready to forge",
                     rarity: Rarity.uncommon,
                     spritePath: "Items/uncommon_ingot",
                     shapeType: ShapeType.Domino) },
            {
                "ore_bloom_common", new StackableItemStats("ore_bloom_common",
                    weight: 5f,
                    goldValue: 1f,
                    description: "an oddly shaped hunk of iron ore for smelting",
                    rarity: Rarity.common,
                    spritePath: "Items/ore_bloom_common",
                    shapeType: ShapeType.Pentomino_X)
            },
            {   "loot_pile_small", new ContainerStats("loot_pile_small",
                    weight: 0f,
                    goldValue: 0f,
                    description: "small pile",
                    spritePath: "Items/loot_pile_small",
                    isQuestItem: true,
                    containerSize: new Coor(r: 3, c: 5))
            },
            {    "loot_pile_medium", new ContainerStats("loot_pile_medium",
                    weight: 0f,
                    goldValue: 0f,
                    description: "average pile",
                    spritePath: "Items/loot_pile_medium",
                    isQuestItem: true,
                    containerSize: new Coor(r: 4, c: 5))
            },
            {    "loot_pile_large", new ContainerStats("loot_pile_large",
                    weight: 0f,
                    goldValue: 0f,
                    description: "large pile",
                    spritePath: "Items/loot_pile_large",
                    isQuestItem: true,
                    containerSize: new Coor(r: 5, c: 6))
            },
            {    "loot_pile_gigantic", new ContainerStats("loot_pile_gigantic",
                    weight: 0f,
                    goldValue: 0f,
                    description: "a truly massive pile",
                    spritePath: "Items/loot_pile_gigantic",
                    isQuestItem: true,
                    containerSize: new Coor(r: 6, c: 12))
            },

            {   "adventure backpack", new EquipableContainerStats("adventure backpack",
                    weight: 2f,
                    goldValue: 5f,
                    description: "a basic adventurer's backpack",
                    spritePath: ShapeType.Monomino.ToString(),
                    isQuestItem: true,
                    shapeType: ShapeType.Monomino,
                    containerSize: new Coor(6, 10),
                    slotType: EquipmentSlotType.Backpack
                )
            },
            {   "small backpack", new EquipableContainerStats("small backpack",
                    weight: 1f,
                    goldValue: 2f,
                    description: "a small backpack",
                    spritePath: ShapeType.Monomino.ToString(),
                    isQuestItem: true,
                    shapeType: ShapeType.Monomino,
                    containerSize: new Coor(4, 6),
                    slotType: EquipmentSlotType.Backpack
                )
            },
            {   "basic_sword_1", new EquipableItemStats("basic_sword_1",
                     weight: 2f,
                     goldValue: 10f,
                     description: "a basic sword",
                     rarity: Rarity.common,
                     spritePath: "Items/basic_sword_1",
                     shapeType: ShapeType.Tromino_I,
                     slotType: EquipmentSlotType.OneHandedWeapon,
                     defaultFacing: Facing.Down,
                     modifiers: new StatModifier[] {
                        new(StatTypes.Strength ,OperatorType.Add,1)}
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
                            new(StatTypes.Strength, OperatorType.Add,15)}
                    )
            },
            {    "basic_crossbow_1", new EquipableItemStats("basic_crossbow_1",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic crossbow",
                    rarity: Rarity.common,
                    spritePath: "Items/basic_crossbow_1",
                    shapeType: ShapeType.Tetromino_T,
                    slotType: EquipmentSlotType.RangedWeapon,
                    defaultFacing: Facing.Right,
                    modifiers: new StatModifier[] {
                            new(StatTypes.Agility, OperatorType.Add,1)}
                    )
            },
            {   "basic_shortsword_0", new EquipableItemStats("basic_shortsword_0",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic shortsword with a sharp edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/common_shortsword_1",
                    shapeType: ShapeType.Domino,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] { }
                    )
            },
            {   "basic_shortsword_1", new EquipableItemStats("basic_shortsword_1",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic shortsword with a refined edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/common_shortsword_1",
                    shapeType: ShapeType.Domino,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(StatTypes.Strength, OperatorType.Add,1)}
                    )
            },
            {   "basic_shortsword_3", new EquipableItemStats("basic_shortsword_3",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic shortsword with a sharp edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/uncommon_shortsword_1",
                    shapeType: ShapeType.Domino,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(StatTypes.Strength, OperatorType.Add,3)}
                    )
            },
            {   "basic_sword_3", new EquipableItemStats("basic_sword_3",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic sword with a sharp edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/uncommon_sword_1",
                    shapeType: ShapeType.Tromino_I,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(StatTypes.Strength, OperatorType.Add,5)}
                    )
            },
            {   "basic_sword_5", new EquipableItemStats("basic_sword_5",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic sword with a sharp edge",
                    rarity: Rarity.rare,
                    spritePath: "Items/basic_sword_1",
                    shapeType: ShapeType.Tromino_I,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(StatTypes.Strength, OperatorType.Add,5)}
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
                        new(StatTypes.Charisma, OperatorType.Add, 5)}
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
                        new(StatTypes.Spirit, OperatorType.Add, 15)}
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
            { "questitem_1", new ItemStats("questitem_1",
                weight: 40f,
                goldValue: 1000f,
                description: "A crank-powered machine of inscrutable purpose.",
                rarity: Rarity.rare,
                spritePath: "Items/questitem_1",
                isQuest: true,
                shape: ShapeType.QuestShape_1)
            },
            {   "pants_1", new EquipableItemStats("pants_1",
                    weight: 0.5f,
                    goldValue: 1f,
                    description: "Clean pants, classic fit.",
                    rarity: Rarity.common,
                    spritePath: "Items/pants_1",
                    shapeType: ShapeType.Monomino,
                    slotType: EquipmentSlotType.Legs)
            },
            {   "shirt_1", new EquipableItemStats("shirt_1",
                    weight: 0.1f,
                    goldValue: 1f,
                    description: "Comfortable shirt",
                    rarity: Rarity.common,
                    spritePath: "Items/shirt_1",
                    shapeType: ShapeType.Monomino,
                    slotType: EquipmentSlotType.InnerTorso)
            }
        };

        public ItemDataSourceTest()
        {

        }

        public IItemStats GetRandomItemStats(Rarity rarity) 
        {
            var itemsOfRarity = itemDictionary.Select(y => y).Where(x => x.Value.Rarity == rarity && x.Value.IsQuestItem == false);
            int randomIndex = Random.Range(0,itemsOfRarity.Count());
            //Debug.Log($"rarity: {rarity}, index: {randomIndex}, itemcount: {itemsOfRarity.Count()}");
            return itemsOfRarity.ElementAt(randomIndex).Value;
        }

        

        public IItemStats GetItemStats(string id)
        {
            if (itemDictionary.ContainsKey(id)) return itemDictionary[id];
            return null;
        }

        
    }
}
