using Data;
using Data.Characters;
using Data.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreationFromScriptableObjects : SceneTestFixture
    {
        readonly string playerId = "player";
        readonly string sceneName = "Test_Characters";

        void CommonInstall()
        {

        }

        [UnityTest]
        public IEnumerator TestCharacterStatRetrievable()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
            var playerStats = characterDataSource.GetById(playerId);
            Assert.That(playerStats is not null);
        }

        [UnityTest]
        public IEnumerator TestCharacterFactoryCreationSuccess()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
            var playerStats = characterDataSource.GetById(playerId);
            var player = CharacterFactory.GetCharacter(playerStats);

            Assert.That(player is not null);
        }

        [UnityTest]
        public IEnumerator TestCreatedCharacterWithInitialStatsSuccess()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
            var playerStats = characterDataSource.GetById(playerId);
            int addedStrength = playerStats.InitialStats[StatTypes.Strength];

            int targetStrength = playerStats.SpeciesBaseStats.BaseStats[StatTypes.Strength] + addedStrength;

            var player = CharacterFactory.GetCharacter(playerStats);

            Assert.IsTrue(player.StatDictionary[StatTypes.Strength].CurrentValue == targetStrength);
        }

        [UnityTest]
        public IEnumerator TestCreatedCharacterWithInitialEquipmentSuccess()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
            var itemDataSource = SceneContainer.Resolve<IItemDataSource>();

            var playerStats = characterDataSource.GetById(playerId);


            var startingEquipment = new IItem[] {
                ItemFactory.GetItem(itemDataSource.GetById("adventure_backpack")),
            };

            var player = CharacterFactory.GetCharacter(
                baseStats: playerStats,
                startingEquipment: startingEquipment);


            Assert.That(player.Backpack.Item.Id == "adventure_backpack");

        }

        [UnityTest]
        public IEnumerator BackpackCapacityIsCorrect()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var itemDataSource = SceneContainer.Resolve<IItemDataSource>();
            var backpackStats = itemDataSource.GetById("adventure_backpack");
            var backpack = ItemFactory.GetItem(backpackStats);
            var container = backpack.Components[typeof(IContainer)] as IContainer;

            IContainerStats containerStats = null;
            foreach (var compontentStats in backpackStats.Components)
            {
                var _containerStats = compontentStats as IContainerStats;
                if (_containerStats is not null)
                {
                    containerStats = _containerStats;
                    break;
                }
            }
            if (containerStats is null)
                Assert.Fail($"containerStats not found on item {backpack.Id}");
            if (container is null)
                Assert.Fail($"container not found on item {backpack.Id}");
            Debug.Log($"stat grid count: {containerStats.Grid.Count}, actual grid count: {container.Grid.Count}");
            Assert.IsTrue(container.Grid.Count == containerStats.Grid.Count);
        }

        [UnityTest]
        public IEnumerator TestCreatedCharacterWithStartingInventorySuccess()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
            var itemDataSource = SceneContainer.Resolve<IItemDataSource>();

            var playerStats = characterDataSource.GetById(playerId);


            var startingEquipment = new List<IItem>(){
                ItemFactory.GetItem(itemDataSource.GetById("adventure_backpack")),
            };

            var startingInventory = new List<IItem>(){
                ItemFactory.GetItem(itemDataSource.GetById("apple_fuji")),
                ItemFactory.GetItem(itemDataSource.GetById("apple_fuji")),
                ItemFactory.GetItem(itemDataSource.GetById("apple_fuji")),
            };

            float targetWeight = startingEquipment[0].Weight;
            foreach (var item in startingInventory)
            {
                targetWeight += item.Weight;
            }
            Debug.Log($"targetWeight: {targetWeight}, starting inventory count: {startingInventory.Count}");


            var player = CharacterFactory.GetCharacter(
                baseStats: playerStats,
                startingEquipment: startingEquipment,
                startingInventory: startingInventory);
            Debug.Log($"player encumbrance: {player.CurrentEncumbrance}, target weight: {targetWeight}");
            Assert.That(Mathf.Abs(player.CurrentEncumbrance - targetWeight) < 0.01);

        }


    }
}