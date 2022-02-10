using Data;
using Data.Interfaces;
using Data.Shapes;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestItem
    {
        Item MyItem;
        ItemStats MyItemStats;
        IDataSource dataSource;

        [SetUp]
        public void SetUp()
        {
            dataSource = new DataSourceTest();
            MyItemStats = (ItemStats)dataSource.GetItemStats("apple_fuji");
            MyItem = (Item)ItemFactory.GetItem(stats: MyItemStats);
        }

        [TearDown]
        public void TearDown()
        {
            MyItem = null;
        }

        [Test]
        public void ItemShapeCorrect()
        {
            Assert.AreEqual(expected: typeof(Square1), actual: MyItem.Shape.GetType());
        }
        

        [Test]
        public void ItemShapeRotateCW()
        {
            Facing initial = MyItem.Shape.CurrentFacing;
            Facing next = (Facing)(((int)initial + 1) % 4);
            MyItem.Shape.Rotate(1);
            Assert.AreEqual(expected: next, actual:MyItem.Shape.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCCW()
        {
            Facing initial = MyItem.Shape.CurrentFacing;
            Facing next = (Facing)(((int)initial - 1) % 4);
            MyItem.Shape.Rotate(-1);
            Assert.AreEqual(expected: next, actual: MyItem.Shape.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCW360()
        {
            Facing initial = MyItem.Shape.CurrentFacing;
            MyItem.Shape.Rotate(1);
            MyItem.Shape.Rotate(1);
            MyItem.Shape.Rotate(1);
            MyItem.Shape.Rotate(1);
            Assert.AreEqual(expected: initial, actual: MyItem.Shape.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCCW360()
        {
            Facing initial = MyItem.Shape.CurrentFacing;
            MyItem.Shape.Rotate(-1);
            MyItem.Shape.Rotate(-1);
            MyItem.Shape.Rotate(-1);
            MyItem.Shape.Rotate(-1);
            Assert.AreEqual(expected: initial, actual: MyItem.Shape.CurrentFacing);
        }
    }
}
