using Data.Items;
using Data.Shapes;
using NUnit.Framework;
using InventoryQuest.Testing.Stubs;

namespace InventoryQuest.Testing
{
    public class TestItem
    {
        IItem MyItem;
        IItemStats MyItemStats;
        IItemDataSource dataSource;

        [SetUp]
        public void SetUp()
        {
            dataSource = new ItemDataSourceTest();
            MyItemStats = dataSource.GetById("apple_fuji");
            MyItem = ItemFactory.GetItem(itemStats: MyItemStats);
        }

        [TearDown]
        public void TearDown()
        {
            MyItem = null;
        }

        [Test]
        public void ItemShapeRotateCW()
        {
            Facing initial = MyItem.CurrentFacing;
            int direction = 1;
            int v = (int)initial + direction;
            var next = v % 4 < 0 ? (Facing)3 : (Facing)(v % 4);
            MyItem.Rotate(direction);
            Assert.AreEqual(expected: next, actual:MyItem.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCCW()
        {
            Facing initial = MyItem.CurrentFacing;
            int direction = -1;
            int v = (int)initial + direction;
            var next = v % 4 < 0 ? (Facing)3 : (Facing)(v % 4);
            MyItem.Rotate(-1);
            Assert.AreEqual(expected: next, actual: MyItem.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCW360()
        {
            Facing initial = MyItem.CurrentFacing;
            MyItem.Rotate(1);
            MyItem.Rotate(1);
            MyItem.Rotate(1);
            MyItem.Rotate(1);
            Assert.AreEqual(expected: initial, actual: MyItem.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCCW360()
        {
            Facing initial = MyItem.CurrentFacing;
            MyItem.Rotate(-1);
            MyItem.Rotate(-1);
            MyItem.Rotate(-1);
            MyItem.Rotate(-1);
            Assert.AreEqual(expected: initial, actual: MyItem.CurrentFacing);
        }
    }
}
