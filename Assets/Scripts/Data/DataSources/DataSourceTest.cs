using Data.Interfaces;
using System;
using System.Collections.Generic;

namespace Data
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

            Dictionary<Type, int> physicalStats = GetStatsBlock(new int[] { 10, 10, 10, 10, 10, 10, 10, 10 });

            return new CharacterStats(name: "[PLAYER NAME]", portraitPath: "Portraits/Enemy 01-1",  stats: physicalStats, equipmentSlots: equipmentSlots);
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
                EquipmentSlotType.RightHand, 
                EquipmentSlotType.LeftHand,
                EquipmentSlotType.Ring,
                EquipmentSlotType.Belt, 
                EquipmentSlotType.Feet
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

        public IItemStats GetItemStats(string id)
        {
            return id switch {
                "apple_fuji" => new ItemStats("apple_fuji",
                     weight: .1f,
                     goldValue: .5f,
                     description: "Fuji Apple, the objectively best apple",
                     spritePath: "Items/Apple_Fuji",
                     shape: ShapeType.Square1),
                "loot_pile" => new ContainerStats("loot_pile",
                    weight: 0f,
                    goldValue: 0f,
                    description: "current loot pile",
                    spritePath: "",
                    containerSize: new Coor(r: 3, c: 5)),
                "thingabob" => new ItemStats("thingabob",
                     weight: 4f,
                     goldValue: 5f,
                     description: "a strange shape",
                     spritePath: "",
                     shape: ShapeType.T1),
                "adventure backpack" => new ContainerStats("adventure backpack",
                    weight: 2f,
                    goldValue: 5f,
                    description: "a basic adventurer's backpack",
                    spritePath: "",
                    shape: ShapeType.Square1,
                    containerSize: new Coor(5, 12)),
                "small backpack" => new ContainerStats("small backpack",
                    weight: 1f,
                    goldValue: 2f,
                    description: "a small backpack",
                    spritePath: "",
                    shape: ShapeType.Square1,
                    containerSize: new Coor(4, 8)),
                "basic_sword_1" => new EquipableItemStats("basic_sword_1",
                     weight: 2f,
                     goldValue: 10f,
                     description: "a basic sword",
                     spritePath: "Items/basic_sword_1",
                     shape: ShapeType.Bar3,
                     slotType: EquipmentSlotType.OneHandedWeapon,
                     defaultFacing: Facing.Down,
                     modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,1)}
                     ),
                "basic_sword_15" => new EquipableItemStats("basic_sword_15",
                    weight: 3.5f,
                    goldValue: 10000f,
                    description: "a basic sword",
                    spritePath: "Items/basic_sword_1",
                    shape: ShapeType.Bar3,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                            new(typeof(Strength),OperatorType.Add,15)}
                    ),
                "basic_crossbow_1" => new EquipableItemStats("basic_crossbow_1",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic crossbow",
                    spritePath: "Items/basic_crossbow_1",
                    shape: ShapeType.T1,
                    slotType: EquipmentSlotType.TwoHandedWeapon,
                    defaultFacing: Facing.Right,
                    modifiers: new StatModifier[] {
                            new(typeof(Agility),OperatorType.Add,1)}
                    ),
                "basic_sword_5" => new EquipableItemStats("basic_sword_5",
                    weight: 2f,
                    goldValue: 10f,
                    description: "a basic sword",
                    spritePath: "",
                    shape: ShapeType.Bar4,
                    slotType: EquipmentSlotType.OneHandedWeapon,
                    defaultFacing: Facing.Down,
                    modifiers: new StatModifier[] {
                        new(typeof(Strength),OperatorType.Add,5)}
                    ),
                "ring_charisma_1" => new EquipableItemStats("ring_charisma_1",
                    weight: 0.1f,
                    goldValue: 50f,
                    description: "simple silver band which grants minor charisma boost",
                    spritePath: "",
                    shape: ShapeType.Square1,
                    slotType: EquipmentSlotType.Ring,
                    modifiers: new StatModifier[] {
                        new(typeof(Charisma), OperatorType.Add, 1)}
                    ),
                _ => new ItemStats("blop",
                     weight: .1f,
                     goldValue: 0f,
                     description: "blip bloop blop",
                     spritePath: "",
                     shape: ShapeType.Square1)
            };
        }

        public CharacterStats GetRandomCharacterStats(Rarity rarity)
        {
            throw new NotImplementedException();
        }
    }


}
