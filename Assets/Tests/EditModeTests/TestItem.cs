using NUnit.Framework;
using UnityEngine;
using Data;
using InventoryQuest;
using InventoryQuest.Characters;
using System;
using InventoryQuest.Shapes;

namespace InventoryQuest.Testing
{
    public class TestItem
    {
        Item MyItem;
        ItemStats MyItemStats = new ItemStats("apple_fuji",
                 weight: .1f,
                 goldValue: .5f,
                 description: "Fuji Apple, the objectively best apple");

        [SetUp]
        public void SetUp()
        {
            var _shape = new Square1();
            MyItem = new Item(id: MyItemStats.ItemId, itemStats: MyItemStats, itemShape: _shape);
        }

        [TearDown]
        public void TearDown()
        {
            MyItem = null;
        }

        [Test]
        public void ItemShapeCorrect()
        {
            Assert.AreEqual(expected: typeof(Square1), actual: MyItem.ItemShape.GetType());
        }
        

        [Test]
        public void ItemShapeRotateCW()
        {
            Facing initial = MyItem.ItemShape.CurrentFacing;
            Facing next = (Facing)(((int)initial + 1) % 4);
            MyItem.ItemShape.Rotate(1);
            Assert.AreEqual(expected: next, actual:MyItem.ItemShape.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCCW()
        {
            Facing initial = MyItem.ItemShape.CurrentFacing;
            Facing next = (Facing)(((int)initial - 1) % 4);
            MyItem.ItemShape.Rotate(-1);
            Assert.AreEqual(expected: next, actual: MyItem.ItemShape.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCW360()
        {
            Facing initial = MyItem.ItemShape.CurrentFacing;
            MyItem.ItemShape.Rotate(1);
            MyItem.ItemShape.Rotate(1);
            MyItem.ItemShape.Rotate(1);
            MyItem.ItemShape.Rotate(1);
            Assert.AreEqual(expected: initial, actual: MyItem.ItemShape.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCCW360()
        {
            Facing initial = MyItem.ItemShape.CurrentFacing;
            MyItem.ItemShape.Rotate(-1);
            MyItem.ItemShape.Rotate(-1);
            MyItem.ItemShape.Rotate(-1);
            MyItem.ItemShape.Rotate(-1);
            Assert.AreEqual(expected: initial, actual: MyItem.ItemShape.CurrentFacing);
        }
    }
}
