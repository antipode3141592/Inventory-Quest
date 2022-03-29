using Data;
using Data.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    public class TestContainer
    {
        IItemDataSource dataSource;
        //container
        Container MyContainer;

        ContainerStats backpackStats;
        //test items
        Item MyItem;
        List<Item> MyItems;
        List<Item> MyItems2;

        int MyTotalItems = 3;
        ItemStats MyItemStats;
        ItemStats MyItemStats2;


        [SetUp]
        public void Setup()
        {
            dataSource = new ItemDataSourceTest();
            MyItemStats = (ItemStats)dataSource.GetItemStats("apple_fuji");
            MyItemStats2 = (ItemStats)dataSource.GetItemStats("brass_trinket");
            backpackStats = (ContainerStats)dataSource.GetItemStats("adventure backpack");
            MyItem = (Item)ItemFactory.GetItem(stats: MyItemStats);
            MyItems = new List<Item>();
            MyItems2 = new List<Item>();
            for (int i = 0; i < MyTotalItems; i++)
            {
                MyItems.Add((Item)ItemFactory.GetItem(stats: MyItemStats));
                MyItems2.Add((Item)ItemFactory.GetItem(stats: MyItemStats2));
            }
            MyContainer = ContainerFactory.GetContainer(stats: backpackStats);
        }

        [TearDown]
        public void TearDown()
        {
            MyContainer = null;
            MyItem = null;
            MyItems = null;
            MyItems2 = null;
        }

        [Test]
        public void ContainerSizeIsCorrect()
        {
            Assert.AreEqual(expected: backpackStats.ContainerSize, actual: MyContainer.ContainerSize);  
        }

        [Test]
        public void NewContainerIsEmpty()
        {
            Assert.IsTrue(IsContainerEmpty(MyContainer));
        }

        public bool IsContainerEmpty(Container container)
        {
            bool isEmpty = true;
            foreach (var square in MyContainer.Grid)
            {
                if (square.IsOccupied == true)
                {
                    isEmpty = false; break;
                }
            }
            return isEmpty;
        }

        [Test]
        public void PlaceAtValidTarget()
        {
            float initialWeight = MyContainer.TotalWeight;
            MyContainer.TryPlace(MyItem, new Coor(0, 0));
            Assert.AreEqual(expected: initialWeight + MyItem.Stats.Weight, actual: MyContainer.TotalWeight);
        }

        [Test]
        public void PlaceSeveralItemsAtValidTargets()
        {
            float initialWeight = MyContainer.TotalWeight;
            float targetWeight = initialWeight + (MyItem.Stats.Weight * 3);
            for (int i = 0; i < MyTotalItems; i++)
            {
                MyContainer.TryPlace(MyItems[i], new Coor(0, 0 + i));
            }
            Assert.AreEqual(expected: targetWeight, actual: MyContainer.TotalWeight);
        }

        [Test]
        public void FailPlaceAtOutOfBoundsTarget()
        {
            Assert.IsFalse(MyContainer.TryPlace(MyItem, new Coor(MyContainer.ContainerSize.row+1, 0)));
        }

        [Test]
        public void FailPlaceAtOccupiedTarget()
        {
            MyContainer.Grid[0, 0].IsOccupied = true;
            Assert.IsFalse(MyContainer.TryPlace(MyItem, new Coor(0, 0)));
        }

        [Test]
        public void FailTakeAtOutOfBoundsTarget()
        {
            Assert.IsFalse(MyContainer.TryTake(out _, new Coor(r: MyContainer.ContainerSize.row + 1, c: 0)));
        }

        [Test]
        public void PlaceAndTakeItem()
        {
            MyContainer.TryPlace(MyItem, new Coor(r: 0, c: 0));
            Assert.IsTrue(MyContainer.TryTake(out _, new Coor(r:0, c:0)));
            Assert.IsTrue(IsContainerEmpty(MyContainer));
        }

        [Test]
        public void PlaceSeveralItems()
        {
            float initialWeight = MyContainer.TotalWeight;
            float targetWeight = initialWeight + (MyItems2[0].Stats.Weight * (float)MyTotalItems);
            for (int i = 0; i < MyTotalItems; i++)
            {
                MyContainer.TryPlace(MyItems2[i], new Coor(r: 0, c: i*2));
            }
            Assert.AreEqual(expected: targetWeight, actual: MyContainer.TotalWeight);
        }

        [Test]
        public void PlaceAndTakeSeveralItems()
        {
            float initialWeight = MyContainer.TotalWeight;
            float targetWeight = initialWeight + (MyItems2[0].Stats.Weight * (float)MyTotalItems);
            for (int i = 0; i < MyTotalItems; i++)
            {
                MyContainer.TryPlace(MyItems2[i], new Coor(r: i*2, c: 0));
                MyContainer.TryTake(out _, new Coor(r: i * 2, c: 0));
            }
            Assert.AreEqual(expected: initialWeight, actual: MyContainer.TotalWeight);
            Assert.IsTrue(IsContainerEmpty(MyContainer));
            
        }


    }
}
