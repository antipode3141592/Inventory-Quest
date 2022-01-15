using Data;
using InventoryQuest.Shapes;
using NUnit.Framework;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    public class TestContainer
    {
        //container
        Container MyContainer;
        Coor backpackSize = new Coor(5, 10);
        ItemStats backpackStats = new ItemStats("adventure backpack",
                weight: 2f,
                goldValue: 5f,
                description: "a basic adventurer's backpack");
        //test items
        Item MyItem;
        List<Item> MyItems;
        int MyTotalItems = 3;
        ItemStats MyItemStats = new ItemStats("apple_fuji",
                 weight: .1f,
                 goldValue: .5f,
                 description: "Fuji Apple, the objectively best apple");

        List<Item> MyItems2;
        ItemStats MyItemStats2 = new ItemStats("thingabob",
                 weight: 4f,
                 goldValue: 5f,
                 description: "a strange shape");


        [SetUp]
        public void Setup()
        {
            MyItem = ItemFactory.GetItem(shape: ShapeType.Square1, stats: MyItemStats);
            MyItems = new List<Item>();
            MyItems2 = new List<Item>();
            for (int i = 0; i < MyTotalItems; i++)
            {
                MyItems.Add(ItemFactory.GetItem(shape: ShapeType.Square1, stats: MyItemStats));
                MyItems2.Add(ItemFactory.GetItem(shape: ShapeType.T1, stats: MyItemStats2));
            }
            MyContainer = ContainerFactory.GetContainer(shape: ShapeType.Square1, stats: backpackStats, containerSize: backpackSize);
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
            Assert.AreEqual(expected: backpackSize, actual: MyContainer.ContainerSize);  
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
            Assert.IsFalse(MyContainer.TryTake(out var item, new Coor(r: MyContainer.ContainerSize.row + 1, c: 0)));
        }

        [Test]
        public void PlaceAndTakeItem()
        {
            MyContainer.TryPlace(MyItem, new Coor(r: 0, c: 0));
            Assert.IsTrue(MyContainer.TryTake(out var item, new Coor(r:0, c:0)));
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
                MyContainer.TryPlace(MyItems2[i], new Coor(r: 0, c: i * 2));
            }

            for (int c = 0; c < MyTotalItems; c++)
            {
                MyContainer.TryTake(out var item, new Coor(r: 1, c: c * 2));
            }
            Assert.IsTrue(IsContainerEmpty(MyContainer));
            Assert.AreEqual(expected: initialWeight, actual: MyContainer.TotalWeight);
        }


    }
}
