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
        IItem EquipableItem;
        List<IItem> BasicItems;
        List<IItem> StackableItems;

        IItemStats BasicItemStats;
        IItemStats EquipableItemStats;
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
            EquipableItemStats = itemDataSource.GetById("basic_crossbow");
            backpackStats = itemDataSource.GetById("adventure_backpack");
            smallBoxStats = itemDataSource.GetById("small_box");

            backpack = ItemFactory.GetItem(itemStats: backpackStats);
            smallBox = ItemFactory.GetItem(itemStats: smallBoxStats);
            MyItem = ItemFactory.GetItem(itemStats: BasicItemStats);
            EquipableItem = ItemFactory.GetItem(itemStats: EquipableItemStats);
        }

        [UnityTest]
        public IEnumerator ContainerDisplayGridCountEqualsContainerGridCount()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            IContainer container = smallBox.Components[typeof(IContainer)] as IContainer;
            if (container is null) Assert.Fail(message: $"item {smallBox.Id} does not have an IContainer component");
            containerDisplay.SetContainer(container);

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
            containerDisplay.SetContainer(container);

            string itemGuId = MyItem.GuId;
            if (container.TryPlace(ref MyItem, new(0, 0)))
            {
                yield return null;
                Assert.IsTrue(containerDisplay.ItemImages.Find(x => x.ItemGuId == itemGuId) is not null);
            }
        }

        [UnityTest]
        public IEnumerator BackpackDisplaysContainedItem()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            IContainer container = backpack.Components[typeof(IContainer)] as IContainer;
            if (container is null) Assert.Fail(message: $"item {smallBox.Id} does not have an IContainer component");
            containerDisplay.SetContainer(container);

            if (container.TryPlace(ref EquipableItem, new(0, 0)))
            {
                yield return null;
                Assert.IsTrue(containerDisplay.ItemImages.Find(x => x.ItemGuId == EquipableItem.GuId) is not null);
            }
        }

        [UnityTest]
        public IEnumerator BackpackDisplaysRotatedContainedItem()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            IContainer container = backpack.Components[typeof(IContainer)] as IContainer;
            if (container is null) Assert.Fail(message: $"item {smallBox.Id} does not have an IContainer component");
            containerDisplay.SetContainer(container);

            EquipableItem.Rotate(1);

            if (container.TryPlace(ref EquipableItem, new(0, 0)))
            {
                yield return null;
                Assert.IsTrue(containerDisplay.ItemImages.Find(x => x.ItemGuId == EquipableItem.GuId) is not null);
            }
        }

        [UnityTest]
        public IEnumerator PlaceMultipleItemsIntoBackpack()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            IContainer container = backpack.Components[typeof(IContainer)] as IContainer;
            if (container is null) Assert.Fail(message: $"item {backpack.Id} does not have an IContainer component");
            containerDisplay.SetContainer(container);


            int itemsToCreate = container.Grid.Count;
            for (int i = 0; i < itemsToCreate; i++)
            {
                ItemPlacementHelpers.TryAutoPlaceToContainer(container: container, item: ItemFactory.GetItem(BasicItemStats));
                yield return null;
            }

            int runningTotal = 0;
            foreach (var content in container.Contents)
            {
                if (content.Value.Item.Id == BasicItemStats.Id)
                {
                    runningTotal += content.Value.Item.Quantity;
                }
            }
            Debug.Log($"{runningTotal} {BasicItemStats.Id} items in {backpack.Id}");
            Assert.IsTrue(runningTotal == itemsToCreate);
        }
    }
}