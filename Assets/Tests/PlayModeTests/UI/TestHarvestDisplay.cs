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

        ICharacterDataSource characterDataSource;
        IHarvestManager harvestManager;
        IPartyManager partyManager;

        ICharacterStats wagonStats;

        void CommonInstall()
        {

        }

        void CommonPostSceneLoadInstall()
        {
            characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();

            harvestManager = SceneContainer.Resolve<IHarvestManager>();
            partyManager = SceneContainer.Resolve<IPartyManager>();

            wagonStats = characterDataSource.GetById("wagon");

            partyManager.AddCharacterToParty(wagonStats);
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

            string harvestPileKey = harvestManager.Piles.First(x => x.Value.Item.Id == "logging_pile").Key;

            harvestManager.SelectPile(harvestPileKey);
            yield return null;
            Debug.Log($"harvest piles: {harvestManager.Piles.Count}");

            if (harvestManager.Piles.Count == 0) Assert.Fail($"incorrect pile count in harvest");

            Debug.Log($"harvet pile contains {harvestManager.Piles[harvestPileKey].Contents.Count} objects");
            Assert.IsTrue(harvestManager.Piles[harvestPileKey].Contents.Count == 10);
            Assert.IsTrue(harvestManager.Piles[harvestPileKey].IsFull);

            void Harvesting_StateEntered(object sender, System.EventArgs e)
            {
                harvestingEntered = true;
            }
        }

        
    }
}