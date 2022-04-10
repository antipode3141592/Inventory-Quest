using Data.Items;
using Data.Shapes;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestItem
    {
        Item MyItem;
        ItemStats MyItemStats;
        IItemDataSource dataSource;

        [SetUp]
        public void SetUp()
        {
            dataSource = new ItemDataSourceTest();
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
            Assert.AreEqual(expected: typeof(Monomino), actual: MyItem.Shape.GetType());
        }
        

        [Test]
        public void ItemShapeRotateCW()
        {
            Facing initial = MyItem.Shape.CurrentFacing;
            int direction = 1;
            int v = (int)initial + direction;
            var next = v % 4 < 0 ? (Facing)3 : (Facing)(v % 4);
            MyItem.Shape.Rotate(direction);
            Assert.AreEqual(expected: next, actual:MyItem.Shape.CurrentFacing);
        }

        [Test]
        public void ItemShapeRotateCCW()
        {
            Facing initial = MyItem.Shape.CurrentFacing;
            int direction = -1;
            int v = (int)initial + direction;
            var next = v % 4 < 0 ? (Facing)3 : (Facing)(v % 4);
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
