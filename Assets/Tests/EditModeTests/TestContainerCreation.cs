using Data.Items;
using InventoryQuest.Testing.Stubs;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryQuest.Testing
{
    public class TestContainerCreation
    {
        IItemDataSource itemDataSource;
        //container
        IItem backpack;
        IItem smallBox;

        IItemStats backpackStats;
        IItemStats smallBoxStats;
        IItemStats simpleItemStats;
        //test items
        IItem simpleItem;

        

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            backpackStats = itemDataSource.GetById("adventure_backpack");
            smallBoxStats = itemDataSource.GetById("small_box");
            simpleItemStats = itemDataSource.GetById("apple_fuji");
            backpack = ItemFactory.GetItem(backpackStats);
            smallBox = ItemFactory.GetItem(smallBoxStats);
            simpleItem = ItemFactory.GetItem(simpleItemStats);

        }

        [TearDown]
        public void TearDown()
        {
            backpackStats = null;
            backpack = null;
            smallBoxStats = null;
            smallBox = null;
            simpleItemStats = null;
            simpleItem = null;

        }

        [Test]
        public void SmallBoxCreationSuccess()
        {
            Assert.IsTrue(smallBox is not null);
        }

        [Test]
        public void BackpackCreationSuccess()
        {
            Assert.IsTrue(backpack is not null);
        }

        [Test]
        public void SmallBoxCapacityIsCorrect()
        {
            var container = smallBox.Components[typeof(IContainer)] as IContainer;
            IContainerStats containerStats = null;
            foreach(var compontentStats in smallBoxStats.Components)
            {
                var _containerStats = compontentStats as IContainerStats;
                if (_containerStats is not null)
                {
                    containerStats = _containerStats;
                    break;
                }
            }
            if (containerStats is null)
                Assert.Fail($"containerStats not found on item {smallBox.Id}");
            if (container is null)
                Assert.Fail($"container not found on item {smallBox.Id}");
            Assert.IsTrue(container.Grid.Count == containerStats.Grid.Count);
        }

        [Test]
        public void BackpackComponentCountCorrect()
        {
            Debug.Log($"item {backpack.Id} has {backpack.Components.Count} components");
            Assert.IsTrue(backpack.Components.Count == backpackStats.Components.Count, message:$"item {backpack.Id} has {backpack.Components.Count} components");
        }

        [Test]
        public void BackpackSlotTypeCorrect()
        {
            var slotType = (backpack.Components[typeof(IEquipable)] as IEquipable).SlotType;
            IEquipableStats equipableStats = null;
            foreach (var component in backpackStats.Components)
            {
                var _equipableStats = component as IEquipableStats;
                if (_equipableStats is not null)
                {
                    equipableStats = _equipableStats;
                }
            }
            if (equipableStats is null)
                Assert.Fail($"equipable stats not found");
            Debug.Log($"item {backpack.Id} has slot type {(backpack.Components[typeof(IEquipable)] as IEquipable).SlotType}");
            Assert.IsTrue((backpack.Components[typeof(IEquipable)] as IEquipable).SlotType == equipableStats.SlotType);
        }

        [Test]
        public void PlaceItemInContainerSuccess()
        {
            IContainer container = smallBox.Components[typeof(IContainer)] as IContainer;
            if (container is null)
                Assert.Fail($"item {smallBox.Id} is missing an IContainer component");
            Assert.IsTrue(container.TryPlace(simpleItem, new Data.Coor(0, 0)));
        }

        [Test]
        public void PlaceItemInContainerAndTakeSuccess()
        {
            IContainer container = smallBox.Components[typeof(IContainer)] as IContainer;
            if (container is null)
                Assert.Fail($"item {smallBox.Id} is missing an IContainer component");

            container.TryPlace(simpleItem, new Data.Coor(0, 0));
            if (!container.TryTake(out IItem item, new Data.Coor(0, 0)))
            {
                Assert.Fail($"item {simpleItemStats.Id} could not be taken from container {smallBox.Id}");
                return;
            }
            Assert.IsTrue(item.Id == simpleItem.Id, message:$"item: {item.Id} placed and taken correctly");
            
        }

        [Test]
        public void AutoPlaceFillContainerSuccess()
        {
            IContainer container = smallBox.Components[typeof(IContainer)] as IContainer;
            
            int itemsToCreate = container.Grid.Count;
            for (int i = 0; i < itemsToCreate; i++)
                ItemPlacementHelpers.TryAutoPlaceToContainer(container: container, item: ItemFactory.GetItem(simpleItemStats));

            int runningTotal = 0;
            foreach (var content in container.Contents)
            {
                if (content.Value.Item.Id == simpleItem.Id)
                {
                    runningTotal += content.Value.Item.Quantity;
                }
            }
            Debug.Log($"{runningTotal} {simpleItemStats.Id} items in {smallBox.Id}");
            Assert.IsTrue(runningTotal == itemsToCreate);

        }
    }
}
