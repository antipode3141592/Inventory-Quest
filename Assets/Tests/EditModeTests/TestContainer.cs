using NUnit.Framework;
using UnityEngine;
using Data;
using InventoryQuest;
using InventoryQuest.Characters;
using InventoryQuest.Shapes;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    public class TestContainer
    {
        //container
        Container MyContainer;
        Vector2Int backpackSize = new Vector2Int(x: 10, y: 5);
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


        [SetUp]
        public void Setup()
        {
            var _shape = new Square1();
            MyItem = new Item(name: MyItemStats.ItemId, itemStats: MyItemStats, itemShape: _shape);
            MyItems = new List<Item>();
            for (int i = 0; i < MyTotalItems; i++)
            {
                MyItems.Add(new Item(name: MyItemStats.ItemId, itemStats: MyItemStats, itemShape: _shape));
            }
            MyContainer = new Container(stats: backpackStats, size: backpackSize);
        }

        [TearDown]
        public void TearDown()
        {
            MyContainer = null;
            MyItem = null;
        }

        [Test]
        public void TestContainerSize()
        {
            Assert.AreEqual(expected: backpackSize, actual: MyContainer.Size);  
        }

        [Test]
        public void TestContainerEmptiness()
        {
            bool isEmpty = true;
            foreach(var square in MyContainer.Grid)
            {
                if (square.IsOccupied == true)
                {
                    isEmpty = false; break;
                }
            }
            Assert.IsTrue(isEmpty);
        }

        [Test]
        public void TryPlaceSuccess()
        {
            float initialWeight = MyContainer.TotalWeight;
            MyContainer.TryPlace(MyItem, new Vector2Int(0, 0));
            Assert.AreEqual(expected: initialWeight + MyItem.ItemStats.Weight, actual: MyContainer.TotalWeight);
        }

        [Test]
        public void TryPlaceSeveralItems()
        {
            float initialWeight = MyContainer.TotalWeight;
            float targetWeight = initialWeight + (MyItem.ItemStats.Weight * 3);
            for (int i = 0; i < MyTotalItems; i++)
            {
                MyContainer.TryPlace(MyItems[i], new Vector2Int(0, 0 + i));
            }
            Assert.AreEqual(expected: targetWeight, actual: MyContainer.TotalWeight);
        }

        [Test]
        public void TryPlaceOutOfBounds()
        {
            Assert.IsFalse(MyContainer.TryPlace(MyItem, new Vector2Int(MyContainer.Size.x+1, 0)));
        }

        [Test]
        public void TryPlaceGridOccupied()
        {
            MyContainer.Grid[0, 0].IsOccupied = true;
            Assert.IsFalse(MyContainer.TryPlace(MyItem, new Vector2Int(0, 0)));
        }

        [Test]
        public void TryTakeOutOfBounds()
        {
            Assert.IsFalse(MyContainer.TryTake(out var item, new Vector2Int(x: MyContainer.Size.x + 1, y: 0)));
        }

        [Test]
        public void TryTakeSuccess()
        {

        }
    }
}
