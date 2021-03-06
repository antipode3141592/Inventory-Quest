using Data;
using Data.Characters;
using Data.Items;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestEquipableItem
    {
        ICharacterDataSource characterDataSource;
        IItemDataSource itemDataSource;

        PlayableCharacter Player;
        EquipableItem Sword;
        CharacterStats playerStats;
        EquipableContainerStats backpackStats;
        EquipableItemStats EquipableStats;
        IEquipable backpack;

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            characterDataSource = new CharacterDataSourceTest();
            playerStats = characterDataSource.GetCharacterStats("Player");
            backpackStats = (EquipableContainerStats)itemDataSource.GetItemStats("adventure backpack");
            backpack = (IEquipable)ItemFactory.GetItem(backpackStats);
            EquipableStats = (EquipableItemStats)itemDataSource.GetItemStats("basic_sword_1");
            Player = CharacterFactory.GetCharacter(playerStats, new IEquipable[] { backpack });
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
            Player.Backpack.TryPlace(Sword, new Coor(0, 0));
            Assert.AreEqual(expectedWeight, Player.CurrentEncumbrance);
        }

        [Test]
        public void TakeItemFromBackpackAndEquip()
        {
            int initialAttackValue = Player.Stats.Attack.CurrentValue;
            int calculatedAttackValue = initialAttackValue + 1;
            Player.Backpack.TryPlace(Sword, new Coor(0, 0));
            string swordId = Sword.GuId;
            
            if (Player.Backpack.TryTake(out var item, new Coor(0, 0)))
            {

                EquipmentSlotType slotType = Sword.SlotType;
                if (Player.EquipmentSlots.ContainsKey(slotType))
                {
                    Player.EquipmentSlots[slotType].TryEquip(out var oldItem, item as IEquipable);
                        
                }

            }
            Assert.AreEqual(swordId, (Player.EquipmentSlots[Sword.SlotType].EquippedItem as IItem).GuId);
            Assert.AreEqual(calculatedAttackValue, Player.Stats.Attack.CurrentValue);
        }

        [Test]
        public void TakeItemFromBackpackEquipAndUnequip()
        {
            float startingStrength = Player.Stats.Strength.CurrentValue;
            Player.Backpack.TryPlace(Sword, new Coor(0, 0));
            EquipmentSlotType slotType = Sword.SlotType;
            string swordId = Sword.GuId;
            if (Player.Backpack.TryTake(out var item, new Coor(0, 0)))
            {

                
                if (Player.EquipmentSlots.ContainsKey(slotType))
                {
                    Player.EquipmentSlots[slotType].TryEquip(out var oldItem, item as IEquipable);
                    Player.EquipmentSlots[slotType].TryUnequip(out var _item);
                }
            }
            Assert.AreEqual(null, Player.EquipmentSlots[slotType].EquippedItem);
            Assert.AreEqual(startingStrength, Player.Stats.Strength.CurrentValue);
        }

    }
}
