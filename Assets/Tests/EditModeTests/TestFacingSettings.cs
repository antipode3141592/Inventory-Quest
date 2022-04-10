using Data;
using Data.Shapes;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestFacingSettings
    {
        [Test]
        public void FacingRight()
        {
            Assert.AreEqual(expected: 0, actual: (int)Facing.Right);
        }

        [Test]
        public void FacingDown()
        {
            Assert.AreEqual(expected: 1, actual: (int)Facing.Down);
        }

        [Test]
        public void FacingLeft()
        {
            Assert.AreEqual(expected: 2, actual: (int)Facing.Left);
        }

        [Test]
        public void FacingUp()
        {
            Assert.AreEqual(expected: 3, actual: (int)Facing.Up);
        }


    }
}