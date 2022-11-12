using Data.Items;
using InventoryQuest.UI;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace InventoryQuest.Testing
{

    public class TestContainerDisplay : SceneTestFixture
    {
        readonly string sceneName = "Test_ContainerDisplay";


        ContainerDisplay containerDisplay;
        IItemDataSource itemDataSource;

        IItem backpack;
        IItem smallBox;

        IItemStats backpackStats;
        IItemStats smallBoxStats;

        IItem MyItem;
        List<IItem> BasicItems;
        List<IItem> StackableItems;

        IItemStats BasicItemStats;
        IItemStats StackableItemStats;

        void CommonInstall()
        {
            BasicItems = new List<IItem>();
            StackableItems = new List<IItem>();
        }

        void CommonPostSceneLoadInstall()
        {
            itemDataSource = SceneContainer.Resolve<IItemDataSource>();
            containerDisplay = SceneContainer.Resolve<ContainerDisplay>();

            BasicItemStats = itemDataSource.GetById("apple_fuji");
            StackableItemStats = itemDataSource.GetById("ingot_common");
            backpackStats = itemDataSource.GetById("adventure backpack");
            smallBoxStats = itemDataSource.GetById("small_box");

            backpack = ItemFactory.GetItem(itemStats: backpackStats);
            smallBox = ItemFactory.GetItem(itemStats: smallBoxStats);
            MyItem = ItemFactory.GetItem(itemStats: BasicItemStats);
        }

        [UnityTest]
        public IEnumerator ContainerDisplayGridCountEqualsContainerGridCount()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            IContainer container = smallBox.Components[typeof(IContainer)] as IContainer;
            if (container is null) Assert.Fail(message: $"item {smallBox.Id} does not have an IContainer component");
            containerDisplay.MyContainer = smallBox.Components[typeof(IContainer)] as IContainer;

            yield return null; //next frame
            Debug.Log($"containerDisplay grid count: {containerDisplay.GetContainerGridCount()}");
            Debug.Log($"container grid count: {(smallBox.Components[typeof(IContainer)] as IContainer).Grid.Count}");
            Assert.IsTrue(containerDisplay.GetContainerGridCount() == (smallBox.Components[typeof(IContainer)] as IContainer).Grid.Count);
        }

        [UnityTest]
        public IEnumerator ContainerDisplaysContainedItem()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            IContainer container = smallBox.Components[typeof(IContainer)] as IContainer;
            if (container is null) Assert.Fail(message: $"item {smallBox.Id} does not have an IContainer component");
            containerDisplay.MyContainer = smallBox.Components[typeof(IContainer)] as IContainer;

            if (container.TryPlace(MyItem, new(0, 0)))
            {
                yield return null;
                Assert.IsTrue(containerDisplay.ItemImages.Find(x => x.ItemGuId == MyItem.GuId) is not null);
            }
        }
    }
}