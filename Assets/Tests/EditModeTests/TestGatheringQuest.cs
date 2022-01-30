using Data;
using InventoryQuest.Characters;
using NUnit.Framework;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    public class TestGatheringQuest
    {
        IQuest quest;
        IDataSource dataSource;
        Party party;
        Character player;
        Character minion_1;
        Character minion_2;

        static int targetCount = 5;
        static string targetItemId = "apple_fuji";

        [SetUp]
        public void SetUp()
        {
            dataSource = new DataSourceTest();
            player = CharacterFactory.GetCharacter(dataSource.GetCharacterStats("Player"),
                (ContainerStats)dataSource.GetItemStats("adventure backpack"));
            minion_1 = CharacterFactory.GetCharacter(dataSource.GetCharacterStats("Minion"),
                (ContainerStats)dataSource.GetItemStats("adventure backpack"));
            minion_2 = CharacterFactory.GetCharacter(dataSource.GetCharacterStats("Minion"),
                (ContainerStats)dataSource.GetItemStats("adventure backpack"));
            party = new Party(new Character[] {player, minion_1, minion_2});
            quest = new GatheringQuest("get_apples", "gather 5 fuji apples", targetItemId, targetCount, "ring_charisma_1");
        }

        [TearDown]
        public void TearDown()
        {
            dataSource = null;
            player = null;
            minion_1 = null;
            minion_2 = null;
            quest = null;
            party = null;
        }

        void AddItems(string itemId, int itemsToMake)
        {
            for (int i = 0; i < itemsToMake; i++)
                party.Characters[i % party.Characters.Count].PrimaryContainer
                     .TryPlace(ItemFactory.GetItem(dataSource.GetItemStats(itemId)), new Coor(0, i));

        }

        [Test]
        public void CreateQuest()
        {
            Assert.IsNotNull(quest);
            
        }

        [Test]
        public void QuestEvaluateTrue()
        {
            AddItems(targetItemId, 5);
            Assert.IsTrue(quest.Evaluate(party));
        }

        [Test]
        public void QuestEvaluateFalse()
        {
            AddItems(targetItemId, 4);
            Assert.IsFalse(quest.Evaluate(party));
        }
    }
}
