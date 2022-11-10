using Data.Items;
using Data.Characters;
using NUnit.Framework;
using System.Collections.Generic;
using InventoryQuest.Testing.Stubs;
using Data;
using UnityEngine;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        ICharacter Player;
        ICharacterStats playerStats;
        IItemStats backpackStats;
        IItem backpack;
        

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            characterDataSource = new CharacterDataSourceTest();
            backpackStats = itemDataSource.GetById("adventure_backpack");
            backpack = ItemFactory.GetItem(backpackStats);
            playerStats = characterDataSource.GetById("player");
            Player = CharacterFactory.GetCharacter(baseStats:playerStats, startingEquipment: new IItem[] { backpack });

        }

        [TearDown]
        public void TearDown()
        {
            Player = null;

        }

        [Test]
        public void PlayerEquipmentSlotsCorrect()
        {
            Assert.AreEqual(expected: playerStats.EquipmentSlotsTypes.Count, actual: Player.EquipmentSlots.Count, message: $"{playerStats.EquipmentSlotsTypes.Count} : {Player.EquipmentSlots.Count}");
        }

        [Test]
        public void PlayerStartingStatsSetCorrectly()
        {
            if (Player.StatDictionary.Count == 0) 
                Assert.Fail("No Stats!");
            foreach (var stat in Player.StatDictionary)
            {
                if (playerStats.InitialStats.ContainsKey(stat.Key) && stat.Value.CurrentValue != playerStats.InitialStats[stat.Key])
                    Assert.Fail($"{stat.Key} not set correctly =>{stat.Value.CurrentValue} :{playerStats.InitialStats[stat.Key]}");
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

        [Test]
        public void StartingEquipmentEquipSuccess()
        {
            if (Player.Backpack is null)
                Assert.Fail($"Backpack reference is Null!");
            Assert.IsTrue(Player.Backpack.Item.Id == backpackStats.Id);
        }

        [Test]
        public void StartingInventorySuccess()
        {
            IItemStats itemStats = itemDataSource.GetById("apple_fuji");
            List<IItem> items = new();
            int itemCount = 3;
            for (int i = 0; i < itemCount; i++)
                items.Add(ItemFactory.GetItem(itemStats));

            Player = CharacterFactory.GetCharacter(
                baseStats: playerStats,
                startingEquipment: new IItem[] { backpack },
                startingInventory: items
            );

            Assert.IsTrue(Player.Backpack.Contents.Count == itemCount);

        }
    }
}
