using NUnit.Framework;
using UnityEngine;
using Data;
using InventoryQuest;
using InventoryQuest.Characters;

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



        [SetUp]
        public void Setup()
        {
            MyContainer = new Container(stats: backpackStats, size: backpackSize);
        }

        [TearDown]
        public void TearDown()
        {
            MyContainer = null;
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
                if (square == true)
                {
                    isEmpty = false; break;
                }
            }
            Assert.IsTrue(isEmpty);
        }

        [Test]
        public void TestContainerTryPlace()
        {

        }
    }
}
