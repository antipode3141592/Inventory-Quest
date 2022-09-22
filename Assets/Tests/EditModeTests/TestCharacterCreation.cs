using Data.Items;
using Data.Characters;
using NUnit.Framework;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        PlayableCharacter Player;
        ICharacterStats playerStats;
        EquipableContainerStats backpackStats;
        IEquipable backpack;

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            characterDataSource = new CharacterDataSourceTest();
            backpackStats = (EquipableContainerStats)itemDataSource.GetItemStats("adventure backpack");
            backpack = (IEquipable)ItemFactory.GetItem(backpackStats);
            playerStats = characterDataSource.GetById("Player");
            Player = (PlayableCharacter)CharacterFactory.GetCharacter(baseStats:playerStats, startingEquipment: new IEquipable[] { backpack });

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

        [Test]
        public void PlayerStartingStatsSetCorrectly()
        {
            if (Player.StatDictionary.Count == 0) 
                Assert.Fail();
            foreach (var stat in Player.StatDictionary)
            {
                if (stat.Value.CurrentValue != Player.StatDictionary[stat.Key].CurrentValue)
                    Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void AddingRanksToPlayerStrengthSuccess()
        {
            int ranks = 2;
            int initiatlStrength = Player.StatDictionary[StatTypes.Strength].CurrentValue;
            PlayableCharacterLeveler.AddRanksToCharacterStat(Player, new Dictionary<StatTypes, int>() { { StatTypes.Strength, ranks } });
            Assert.AreEqual(ranks + initiatlStrength, Player.StatDictionary[StatTypes.Strength].CurrentValue);
        }
    }
}
