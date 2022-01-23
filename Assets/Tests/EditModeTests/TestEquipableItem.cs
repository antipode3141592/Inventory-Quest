using Data;
using InventoryQuest.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestEquipableItem
    {
        IDataSource dataSource;
        Character Player;
        EquipableItem Sword;
        CharacterStats playerStats;
        ContainerStats backpackStats;
        EquipableItemStats EquipableStats;

        [SetUp]
        public void SetUp()
        {
            dataSource = new DataSourceTest();
            playerStats = dataSource.GetCharacterStats("Player");
            backpackStats = (ContainerStats)dataSource.GetItemStats("adventure backpack");
            EquipableStats = (EquipableItemStats)dataSource.GetItemStats("basic_sword_1");
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
                        
                    }
                }
            }
            Assert.AreEqual(swordId, Player.EquipmentSlots[EquipmentSlotType.RightHand].EquippedItem.Id);
            Assert.AreEqual(11f, Player.Stats.Strength.CurrentValue);
        }

        [Test]
        public void TakeItemFromBackpackEquipAndUnequip()
        {
            float startingStrength = Player.Stats.Strength.CurrentValue;
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
                        var __item = Player.EquipmentSlots[EquipmentSlotType.RightHand].Unequip();
                    }
                }
            }
            Assert.AreEqual(null, Player.EquipmentSlots[EquipmentSlotType.RightHand].EquippedItem);
            Assert.AreEqual(startingStrength, Player.Stats.Strength.CurrentValue);
        }

    }
}
