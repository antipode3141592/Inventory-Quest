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

        [Test]
        public void PlayerStartingStatsSetCorrectly()
        {
            if (Player.Stats.StatDictionary.Count == 0) 
                Assert.Fail();
            foreach (var stat in playerStats.StatDictionary)
            {
                if (stat.Value.CurrentValue != Player.Stats.StatDictionary[stat.Key].CurrentValue)
                    Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void AddingRanksToPlayerStrengthSuccess()
        {
            int ranks = 2;
            int initiatlStrength = Player.Stats.Strength.CurrentValue;
            PlayableCharacterLeveler.AddRanksToCharacterStat(Player, new Dictionary<CharacterStatTypes, int>() { { CharacterStatTypes.Strength, ranks } });
            Assert.AreEqual(ranks + initiatlStrength, Player.Stats.Strength.CurrentValue);
        }
    }
}
