using Data;
using InventoryQuest.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        Character Player;
        CharacterStats playerStats = new CharacterStats(10f, 10f, 10f);
        Coor backpackSize = new Coor(r: 5, c: 10);
        ItemStats backpackStats = new ItemStats("adventure backpack",
                weight: 2f,
                goldValue: 5f,
                description: "a basic adventurer's backpack");

        [SetUp]
        public void SetUp()
        {
            Player = new Character(ContainerFactory.GetContainer(ShapeType.Square1, backpackStats, backpackSize), playerStats);

        }

        [TearDown]
        public void TearDown()
        {
            Player = null;

        }

        [Test]
        public void PlayerCreationBackpackSize()
        {
            Assert.AreEqual(expected: backpackSize, actual: Player.PrimaryContainer.ContainerSize);
        }
    }
}
