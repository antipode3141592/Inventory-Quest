using Data;
using InventoryQuest.Characters;
using NUnit.Framework;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        IDataSource dataSource;
        Character Player;
        CharacterStats playerStats;
        ContainerStats backpackStats;

        [SetUp]
        public void SetUp()
        {
            dataSource = new DataSourceTest();
            backpackStats = (ContainerStats)dataSource.GetItemStats("adventure backpack");
            playerStats = dataSource.GetCharacterStats("Player");
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
