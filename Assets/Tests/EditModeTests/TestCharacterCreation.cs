using Data;
using InventoryQuest.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestCharacterCreation
    {
        Character Player;
        static EquipmentSlotType[] equipmentSlots = { EquipmentSlotType.Belt, EquipmentSlotType.Feet };
        CharacterStats playerStats = new(10f, 10f, 10f, equipmentSlots:equipmentSlots);
        
        static Coor backpackSize = new(r: 5, c: 12);
        ContainerStats backpackStats = new ContainerStats("adventure backpack",
                weight: 2f,
                goldValue: 5f,
                description: "a basic adventurer's backpack",
                containerSize: backpackSize);

        [SetUp]
        public void SetUp()
        {
            Player = CharacterFactory.GetCharacter(playerStats, backpackStats);

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

        [Test]
        public void PlayerEquipmentSlotsCorrect()
        {
            Assert.AreEqual(expected: equipmentSlots.Length, actual: Player.EquipmentSlots.Count);
        }
    }
}
