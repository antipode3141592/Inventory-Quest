using Data;
using InventoryQuest.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        Character Player;
        CharacterStats playerStats = new CharacterStats(10, 10, 10);
        Coor backpackSize = new Coor(r: 5, c: 10);
        ItemStats backpackStats = new ItemStats("adventure backpack",
                weight: 2f,
                goldValue: 5f,
                description: "a basic adventurer's backpack");

        [Test]
        public void PlayerCreationBackpackSize()
        {
            CreateDefaultPlayer();
            Assert.AreEqual(expected: backpackSize, actual: Player.PrimaryContainer.Size);
        }





        public void CreateDefaultPlayer()
        {
            Player = new Character(new Container(backpackStats, backpackSize), playerStats);
        }
    }
}
