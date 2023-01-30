using Data;
using Data.Items;
using Data.Shapes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InventoryQuest.Testing.Stubs
{
    class ItemDataSourceTest: IItemDataSource
    {
        IItemStats _defaultItem;

        Dictionary<string, IItemStats> _itemStats = new()
        {
            {
                "apple_fuji",
                new ItemStats(
                    id: "apple_fuji",
                    name: "Fuji Apple",
                    weight: .1f,
                    individualGoldValue: .5f,
                    description: "The objectively best apple.",
                    rarity: Rarity.common,
                    sprite: Resources.Load<Sprite>("Items/Apple_Fuji"),
                    defaultFacing: Facing.Right,
                    shape: new Monomino(),
                    isQuestItem: false,
                    components: new List<IItemComponentStats>())
            },
            {
                "basic_sword_15",
                new ItemStats("basic_sword_15",
                    name: "Sword +15",
                    weight: 3.5f,
                    individualGoldValue: 10000f,
                    description: "a basic sword",
                    rarity: Rarity.epic,
                    sprite: Resources.Load<Sprite>("Items/basic_sword_1"),
                    shape: new Monomino(),
                    defaultFacing: Facing.Down,
                    isQuestItem: false,
                    components: new List<IItemComponentStats>() {
                        new EquipableStats(
                            slotType: EquipmentSlotType.OneHandedWeapon,
                            modifiers: new List<StatModifier>() {
                                new(StatTypes.Strength, OperatorType.Add, 15)
                            }
                        )
                    }

                )
            },
            {
                "adventure_backpack",
                new ItemStats("adventure_backpack",
                    name: "Adventure Backpack",
                    weight: 2f,
                    individualGoldValue: 5f,
                    description: "a basic adventurer's backpack",
                    sprite: null,
                    isQuestItem: false,
                    shape: new Monomino(),
                    defaultFacing: Facing.Right,
                    rarity: Rarity.common,
                    components: new List<IItemComponentStats>()
                    {
                        new EquipableStats(EquipmentSlotType.Backpack,
                            new List<StatModifier>()),
                        new ContainerStats(GenerateSimpleGrid(4,6))
                    }
                )
            },
            {
                "small_box",
                new ItemStats("small_box",
                    name: "Small Box",
                    weight: 2f,
                    individualGoldValue: 5f,
                    description: "It's bigger on the inside.",
                    sprite: Resources.Load<Sprite>("Items/loot_pile_large"),
                    isQuestItem: true,
                    shape: new Monomino(),
                    defaultFacing: Facing.Right,
                    rarity: Rarity.common,
                    components: new List<IItemComponentStats>(){
                        new ContainerStats(GenerateSimpleGrid(2,2))
                    }
                )
            }
        };

        static List<Coor> GenerateSimpleGrid(int rows, int columns)
        {
            var coorList = new List<Coor>();
            for(int r = 0; r < rows; r++)
                for(int c = 0; c < columns; c++)
                    coorList.Add(new Coor(r, c));
            return coorList;
        }

        public IItemStats GetById(string id)
        {
            if (_itemStats.ContainsKey(id))
                return _itemStats[id];
            return _defaultItem;
        }

        public IItemStats GetRandom()
        {
            return null;
        }

        public IItemStats GetItemByRarity(Rarity rarity)
        {
            var itemsOfRarity = _itemStats.Select(y => y).Where(x => x.Value.Rarity == rarity && x.Value.IsQuestItem == false);
            int randomIndex = Random.Range(0, itemsOfRarity.Count());
            return itemsOfRarity.ElementAt(randomIndex).Value;
        }
    }
}
