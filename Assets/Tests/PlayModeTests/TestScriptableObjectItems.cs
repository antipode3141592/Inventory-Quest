using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Data.Items;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestScriptableObjectItems : SceneTestFixture
    {
        readonly string itemId = "apple_fuji";
        readonly string sceneName = "Test_Items";

        void CommonInstall()
        {

        }

        [UnityTest]
        public IEnumerator TestCreatedItemIdCorrect()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var itemDataSource = SceneContainer.Resolve<IItemDataSource>();
            var itemStats = itemDataSource.GetById(itemId);
            var item = ItemFactory.GetItem(itemStats);
            Assert.IsTrue(item.Id == itemStats.Id);
        }

        [UnityTest]
        public IEnumerator TestItemShapeCorrect()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var itemDataSource = SceneContainer.Resolve<IItemDataSource>();
            var itemStats = itemDataSource.GetById(itemId);
            var item = ItemFactory.GetItem(itemStats);
            Debug.Log($"item {item.Id} has shape {item.Shape.Id} and {item.Shape.Points.Count} points");
            Assert.IsTrue(item.Shape.Id == itemStats.Shape.Id);
        }
    }
}
