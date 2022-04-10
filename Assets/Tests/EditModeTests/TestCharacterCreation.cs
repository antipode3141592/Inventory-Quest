using Data.Items;
using Data.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        PlayableCharacter Player;
        CharacterStats playerStats;
        EquipableContainerStats backpackStats;
        IEquipable backpack;

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            characterDataSource = new CharacterDataSourceTest();
            backpackStats = (EquipableContainerStats)itemDataSource.GetItemStats("adventure backpack");
            backpack = (IEquipable)ItemFactory.GetItem(backpackStats);
            playerStats = characterDataSource.GetCharacterStats("Player");
            Player = CharacterFactory.GetCharacter(characterStats:playerStats, startingEquipment: new IEquipable[] { backpack });

        }

        [TearDown]
        public void TearDown()
        {
            Player = null;

        }

        [Test]
        public void PlayerCreationBackpackSize()
        {
            Assert.AreEqual(expected: backpackStats.ContainerSize, actual: Player.Backpack.Dimensions);
        }

        [Test]
        public void PlayerEquipmentSlotsCorrect()
        {
            Assert.AreEqual(expected: playerStats.EquipmentSlotsTypes.Count, actual: Player.EquipmentSlots.Count);
        }
    }
}
