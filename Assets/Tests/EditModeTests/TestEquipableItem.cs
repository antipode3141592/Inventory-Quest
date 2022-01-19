using Data;
using InventoryQuest.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestEquipableItem
    {

        Character Player;
        EquipableItem Sword;
        
        static EquipmentSlotType[] equipmentSlots = { EquipmentSlotType.Belt, EquipmentSlotType.Feet , EquipmentSlotType.RightHand};
        CharacterStats playerStats = new(10f, 10f, 10f, equipmentSlots: equipmentSlots);

        static Coor backpackSize = new(r: 5, c: 12);
        ContainerStats backpackStats = new ContainerStats("adventure backpack",
                weight: 2f,
                goldValue: 5f,
                description: "a basic adventurer's backpack",
                containerSize: backpackSize);

        static StatModifier[] statMods = { 
            new( "Attack", OperatorType.Add, 5f),
            new("Strength",OperatorType.Add,1f)
        };
        EquipableItemStats EquipableStats = new EquipableItemStats("basic_sword_1",
                 weight: 2f,
                 goldValue: 10f,
                 description: "a basic sword",
                 shape: ShapeType.Bar3,
                 defaultFacing: Facing.Down,
                 modifiers: statMods);

        [SetUp]
        public void SetUp()
        {
            Player = CharacterFactory.GetCharacter(playerStats, backpackStats);
            Sword = (EquipableItem)ItemFactory.GetItem(EquipableStats); 
        }

        [TearDown]
        public void TearDown()
        {
            Player = null;
            Sword = null;
        }

        [Test]
        public void CreateEquipableItem()
        {
            Assert.IsTrue(Sword is EquipableItem);
        }

        [Test]
        public void AddEquipableItemToBackpack()
        {
            float expectedWeight = backpackStats.Weight + EquipableStats.Weight;
            Player.PrimaryContainer.TryPlace(Sword, new Coor(0, 0));
            Assert.AreEqual(expectedWeight, Player.CurrentEncumbrance);
        }

        [Test]
        public void TakeItemFromBackpackAndEquip()
        {
            Player.PrimaryContainer.TryPlace(Sword, new Coor(0, 0));
            string swordId = Sword.Id;
            if (Player.PrimaryContainer.TryTake(out var item, new Coor(0, 0)))
            {
                EquipableItem _item = item as EquipableItem;
                if (_item != null)
                {
                    if (Player.EquipmentSlots.ContainsKey(EquipmentSlotType.RightHand))
                    {
                        Player.EquipmentSlots[EquipmentSlotType.RightHand].TryEquip(out var oldItem, _item);
                        Assert.AreEqual(swordId, Player.EquipmentSlots[EquipmentSlotType.RightHand].EquippedItem.Id);
                    }
                }
            }
        }
    }
}
