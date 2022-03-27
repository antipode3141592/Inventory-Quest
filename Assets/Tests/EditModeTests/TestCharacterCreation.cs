using Data;
using Data.Interfaces;
using InventoryQuest.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        Character Player;
        CharacterStats playerStats;
        ContainerStats backpackStats;

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            characterDataSource = new CharacterDataSourceTest();
            backpackStats = (ContainerStats)itemDataSource.GetItemStats("adventure backpack");
            playerStats = characterDataSource.GetCharacterStats("Player");
            Player = CharacterFactory.GetCharacter(playerStats, backpackStats);

        }

        [TearDown]
        public void TearDown()
        {
            Player = null;

        }

        [Test]
        public void PlayerCreationBackpackSize()
        {
            Assert.AreEqual(expected: backpackStats.ContainerSize, actual: Player.PrimaryContainer.ContainerSize);
        }

        [Test]
        public void PlayerEquipmentSlotsCorrect()
        {
            Assert.AreEqual(expected: playerStats.EquipmentSlotsTypes.Count, actual: Player.EquipmentSlots.Count);
        }
    }
}
