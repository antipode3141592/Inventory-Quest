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
        
        static Coor backpackSize = new(r: 5, c: 12);
        ContainerStats backpackStats = new ContainerStats("adventure backpack",
                weight: 2f,
                goldValue: 5f,
                description: "a basic adventurer's backpack",
                containerSize: backpackSize);

        [SetUp]
        public void SetUp()
        {
            dataSource = (IDataSource)new DataSourceTest();
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
            Assert.AreEqual(expected: backpackSize, actual: Player.PrimaryContainer.ContainerSize);
        }

        [Test]
        public void PlayerEquipmentSlotsCorrect()
        {
            Assert.AreEqual(expected: playerStats.EquipmentSlots.Count, actual: Player.EquipmentSlots.Count);
        }
    }
}
