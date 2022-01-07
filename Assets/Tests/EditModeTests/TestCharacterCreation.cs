using NUnit.Framework;
using UnityEngine;
using Data;
using InventoryQuest;
using InventoryQuest.Characters;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        Character Player;
        CharacterStats playerStats = new CharacterStats(10, 10, 10);
        Vector2Int backpackSize = new Vector2Int(x: 10, y: 5);
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
