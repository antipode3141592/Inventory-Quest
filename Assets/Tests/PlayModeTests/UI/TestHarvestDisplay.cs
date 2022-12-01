using Data.Characters;
using Data.Items;
using InventoryQuest.Managers;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace InventoryQuest.Testing
{
    public class TestHarvestDisplay : SceneTestFixture
    {
        readonly string sceneName = "Test_HarvestDisplay";

        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        IHarvestManager harvestManager;
        IPartyManager partyManager;

        IContainer loggingPile;

        ICharacter wagon;
        ICharacterStats wagonStats;

        IItem log;

        List<IItem> logs;

        IItemStats logStats;
        IItemStats cookieStats;
        IItemStats wagonContainerStats;

        IItem wagonContainer;

        void CommonInstall()
        {
            logs = new List<IItem>();
        }

        void CommonPostSceneLoadInstall()
        {
            itemDataSource = SceneContainer.Resolve<IItemDataSource>();
            characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();

            harvestManager = SceneContainer.Resolve<IHarvestManager>();
            partyManager = SceneContainer.Resolve<IPartyManager>();

            wagonStats = characterDataSource.GetById("wagon");
            wagonContainerStats = itemDataSource.GetById("wagon_standard");

            wagonContainer = ItemFactory.GetItem(itemStats: wagonContainerStats);
            wagon = CharacterFactory.GetCharacter(baseStats: wagonStats, startingEquipment: new IItem[] { wagonContainer });

            partyManager.AddCharacterToParty(wagon);
        }

        [UnityTest]
        public IEnumerator TestHarvestLoadSuccess()
        {
            bool harvestingEntered = false;

            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            harvestManager.BeginHarvest(harvestType: HarvestTypes.Forest);
            

            harvestManager.Harvesting.StateEntered += Harvesting_StateEntered;

            while (!harvestingEntered)
                yield return null;

            harvestManager.SelectPile(harvestManager.Piles.Keys.ElementAt(0));
            yield return null;
            Debug.Log($"harvest piles: {harvestManager.Piles.Count}");

            if (harvestManager.Piles.Count == 0) Assert.Fail($"incorrect pile count in harvest");

            Debug.Log($"harvet pile contains {harvestManager.Piles[harvestManager.Piles.Keys.ElementAt(0)].Contents.Count} objects");
            Assert.IsTrue(harvestManager.Piles[harvestManager.Piles.Keys.ElementAt(0)].Contents.Count == 10);
            Assert.IsTrue(harvestManager.Piles[harvestManager.Piles.Keys.ElementAt(0)].IsFull);

            void Harvesting_StateEntered(object sender, System.EventArgs e)
            {
                harvestingEntered = true;
            }
        }

        
    }
}